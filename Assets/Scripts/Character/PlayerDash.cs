using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;

    public bool isDashing;
    private int nbDash;
    private float timer = 0;

    public void Dash()
    {
        if (nbDash == 0 || isDashing) return;
        if(rb2d.velocity.sqrMagnitude > 0) StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        nbDash--;
        isDashing = true;
        
        rb2d.velocity = new Vector2(rb2d.velocity.x,0) * data.speedMultiplier;

        float timeElapsed = 0;
        while (timeElapsed < data.dashDuration)
        {
            timeElapsed += Time.deltaTime;
            rb2d.velocity = new Vector2(rb2d.velocity.x,0);
            yield return new WaitForEndOfFrame();
        }
        
        rb2d.velocity /= data.speedMultiplier;
        isDashing = false;
    }

    public void RefreshDash()
    {
        nbDash = 1;
    }
}
