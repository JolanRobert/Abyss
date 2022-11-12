using UnityEngine;

public class PressButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite pressedSprite;
    
    private bool isPressed;

    private void Press()
    {
        isPressed = true;
        spriteRenderer.sprite = pressedSprite;
        onButtonPressed.Invoke(this);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isPressed) return;

        if (col.TryGetComponent(out PlayerController player))
        {
            Press();
        }
    }
    
    public delegate void OnButtonPressed(PressButton button);
    public static OnButtonPressed onButtonPressed;
}
