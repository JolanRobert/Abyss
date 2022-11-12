using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (playerController != null) 
        {
            playerController.SetCheckPoint(transform.position);
        }
    }
}
