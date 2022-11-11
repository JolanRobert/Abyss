using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;

    public bool isDashing;
    private bool canDash = true;
    private int nbDash;

    public void Dash()
    {
        if (nbDash == 0 || isDashing) return;
        if(rb2d.velocity.sqrMagnitude > 0) StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;
        canDash = false;
        nbDash--;

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
        
        yield return new WaitForSeconds(data.dashCooldown);
        canDash = true;
    }

    public void RefreshDash()
    {
        if (!canDash) return;
        nbDash = 1;
    }
}
