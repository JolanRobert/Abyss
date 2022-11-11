using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;
    
    public void Dash()
    {
        
    }
}
