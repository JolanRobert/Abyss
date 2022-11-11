using UnityEngine;

[CreateAssetMenu(menuName = "Abyss/PlayerData", fileName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement")]
    public AnimationCurve moveCurve;

    [Header("Jump")]
    public float jumpForce;
    public int nbJump;
    
    [Header("Dash")]
    public float dashDuration;
    public float speedMultiplier;

    [Header("Light")]
    public float nbLight;
    public float lightDuration;
}
