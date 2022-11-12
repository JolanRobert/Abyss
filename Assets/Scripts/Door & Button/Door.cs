using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text buttonCountText;
    [SerializeField] private List<PressButton> buttons;

    //private bool isOpen;

    private void Start()
    {
        buttonCountText.text = $"0/{buttons.Count}";
    }

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
        buttonCountText.text = $"0/{buttons.Count}";
        if (buttons.Count == 0)
        {
            buttonCountText.gameObject.SetActive(false);
            Open();
        }
    }

    private void Open()
    {
        //isOpen = true;
        animator.Play("Open");
        boxCollider.isTrigger = true;
    }
}
