using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private List<PressButton> buttons;

    //private bool isOpen;

    private void OnEnable()
    {
        PressButton.onButtonPressed += ValidateButton;
    }

    private void OnDisable()
    {
        PressButton.onButtonPressed -= ValidateButton;
    }

    private void ValidateButton(PressButton button)
    {
        buttons.Remove(button);
        if (buttons.Count == 0) Open();
    }

    private void Open()
    {
        //isOpen = true;
        animator.Play("Open");
        boxCollider.isTrigger = true;
    }
}
