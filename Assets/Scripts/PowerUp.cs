using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour
{
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerStayEvent;
    public UnityEvent triggerExitEvent;

    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            triggerEnterEvent.Invoke();
            Depop();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            triggerStayEvent.Invoke();
            Depop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            triggerExitEvent.Invoke();
            Depop();
        }
    }

    private void Depop() 
    {
        transform.gameObject.SetActive(false);
    }
}
