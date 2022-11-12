using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private PlayerController playerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        playerController = collision.GetComponent<PlayerController>();
        if (playerController != null) 
        {
            playerController.SetCheckPoint(transform.position);
        }
    }
}
