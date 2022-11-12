using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float timeOffset;
    [SerializeField] private int idx = 0;
    [SerializeField] private Animator animator;

    private Vector2 nextPosition;
    private PlayerController.AnimType animType = PlayerController.AnimType.Idle;
    private PlayerController.AnimType nextAnimType = PlayerController.AnimType.Idle;
    private PlayerController.InputElement inputElement;

    public bool canHurt;

    private void Start()
    {
        nextPosition = transform.position;
    }

    void Update()
    {
        inputElement = playerController.inputBuffer[idx];
        if (Time.time - timeOffset > inputElement.inputTime) 
        {
            nextPosition = inputElement.playerPosition;
            nextAnimType = inputElement.animType;
            idx = ++idx % playerController.inputBuffer.Length;
        }
        //nextPosition.x - transform.position.x
        //transform.position.x - nextPosition.x
        if (!(nextPosition.x - transform.position.x < 0.1f && nextPosition.x - transform.position.x > -0.1f))
        {
            transform.localScale = new Vector3(Mathf.Sign(nextPosition.x - transform.position.x), 1, 1);
        }


        //Debug.Log(nextAnimType + "\n" + animType);
        //Debug.Log("test : " + (Time.time - timeOffset) + "\n tab : " + playerController.inputBuffer[idx].inputTime);
        transform.position = Vector2.Lerp(transform.position, nextPosition, 1f);
        ChangeAnimationState(nextAnimType);
    }

    private void ChangeAnimationState(PlayerController.AnimType nextAnimType)
    {
        if (animType == nextAnimType) return;

        //Debug.Log(Time.time + " : change");
        animator.Play(nextAnimType.ToString());
        animType = nextAnimType;
    }
}
