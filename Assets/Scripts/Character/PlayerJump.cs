using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded => IsGrounded();
    
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckOffsetX;
    [SerializeField] private LayerMask layerMask;
    
    private PlayerData data => playerController.data;

    private int nbJumpLeft;
    
    public void Jump()
    {
        if (!IsGrounded() && nbJumpLeft <= 0) return;
        rb2d.velocity = new Vector2(rb2d.velocity.x, data.jumpForce);
        nbJumpLeft--;
    }

    public void CancelJump()
    {
        if (rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y/2);
        }
    }

    public void RefreshJumps()
    {
        if (isGrounded && rb2d.velocity.y <= 0)
        {
            nbJumpLeft = data.nbJump;
        }
    }

    private bool IsGrounded()
    {
        Vector2 leftPosition = (Vector2)groundCheck.position - new Vector2(groundCheckOffsetX, 0);
        Vector2 rightPosition = (Vector2)groundCheck.position + new Vector2(groundCheckOffsetX, 0);
        
        bool left = Physics2D.Raycast(leftPosition, Vector2.down, 0.05f, layerMask);
        bool right = Physics2D.Raycast(rightPosition, Vector2.down, 0.05f, layerMask);
        
        Debug.DrawRay(leftPosition, Vector3.down*0.1f, left ? Color.green : Color.red);
        Debug.DrawRay(rightPosition, Vector3.down*0.1f, right ? Color.green : Color.red);

        return left || right;
    }
}
