using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float timeOffset;
    private int idx = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float inoffensiveAlpha;
    [SerializeField] private float fadeTime;

    private Vector2 nextPosition;
    private PlayerController.AnimType animType = PlayerController.AnimType.Idle;
    private PlayerController.AnimType nextAnimType = PlayerController.AnimType.Idle;
    private PlayerController.InputElement inputElement;

    public bool canHurt = false;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.CompareTag("SafeZone")) 
        {
            canHurt = false;
            StartCoroutine(ShadowFadeCoroutine(fadeTime, inoffensiveAlpha));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.transform.CompareTag("SafeZone")) 
        {
            canHurt = true;
            StartCoroutine(ShadowFadeCoroutine(fadeTime, 1));
        }
    }

    private float timer;
    IEnumerator ShadowFadeCoroutine(float t, float alpha) 
    {
        timer = 0;
        while (timer < t) 
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
            //Debug.Log(timer / t);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(sprite.color.a, alpha, timer / t));
        }
        if (alpha > 0.9) 
        {
            if ((playerController.transform.position - transform.position).magnitude < 0.4) 
            {
                playerController.Respawn();
            }
        }
    }
}
