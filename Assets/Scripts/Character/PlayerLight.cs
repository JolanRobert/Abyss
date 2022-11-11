using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private PlayerData data => playerController.data;

    public void Light()
    {
        
    }
}
