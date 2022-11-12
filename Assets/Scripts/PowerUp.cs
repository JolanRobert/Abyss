using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    
    private PlayerController playerController;

    private bool isDepop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            triggerEnterEvent.Invoke();
            Depop();
        }
        
    }

    private void Depop()
    {
        if (isDepop) return;
        
        isDepop = true;
        sprite.SetActive(false);
    }
    
    public UnityEvent triggerEnterEvent;
}
