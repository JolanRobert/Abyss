using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;

    private float timer = 0;

    public void Move(float moveInput)
    {
        if (rb2d.velocity.x >= 0 && moveInput > 0)
        {
            timer += Time.deltaTime;
            rb2d.velocity = new Vector2(data.moveCurve.Evaluate(timer), rb2d.velocity.y);
        }
        else if (rb2d.velocity.x <= 0 && moveInput < 0)
        {
            timer += Time.deltaTime;
            rb2d.velocity = new Vector2(-data.moveCurve.Evaluate(timer), rb2d.velocity.y);
        }
        else 
        {
            timer = 0;
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (!(moveInput < 0.1f && moveInput > -0.1f))
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }

    }
}
