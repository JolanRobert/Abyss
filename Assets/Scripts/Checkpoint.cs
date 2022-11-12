using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject light;

    private bool isActive;

    private void Activate()
    {
        if (isActive) return;
        isActive = true;
        light.SetActive(true);
        animator.Play("Idle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.SetCheckPoint((Vector2)transform.position+Vector2.up*1.5f);
            Activate();
        }
    }
}
