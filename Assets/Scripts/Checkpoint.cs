using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private bool isActive;

    public void Activate()
    {
        if (isActive) return;
        isActive = true;
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
