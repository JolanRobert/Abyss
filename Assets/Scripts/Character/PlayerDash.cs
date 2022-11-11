using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;


    private float timer = 0;

    public void Dash()
    {
        if(rb2d.velocity.sqrMagnitude > 0) StartCoroutine(dashCoroutine(data.dashDuration));
    }

    IEnumerator dashCoroutine(float t) 
    {
        Debug.Log("dashing");
        rb2d.velocity = rb2d.velocity * data.speedMultiplier;
        playerController.isDashing = true;
        yield return new WaitForSeconds(t);
        Debug.Log("End Dashing");
        rb2d.velocity = rb2d.velocity / data.speedMultiplier;
        playerController.isDashing = false;
    }
}
