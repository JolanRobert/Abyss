using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;
    
    public void Jump()
    {
        
    }

    public void CancelJump()
    {
        
    }
}
