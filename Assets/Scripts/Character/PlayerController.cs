using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData data;
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerLight playerLight;

    private PlayerInputs inputs;
    private float moveInput;
    
    //private List<InputElement> inputBuffer;

    private void Start()
    {
        inputs = new PlayerInputs();
        inputs.Enable();
    }

    //private InputElement inputElement = new();
    private void Update()
    {
        //inputElement.inputTime = Time.time % 5;
        //inputElement.playerPosition = transform.position;
        
        ApplyMovement();
        ApplyJump();
        ApplyDash();
        ApplyLight();
    }

    private void ApplyMovement()
    {
        moveInput = inputs.Player.Move.ReadValue<float>();
        playerMovement.Move(moveInput);

        //inputElement.animType = (moveInput < 0.1f && moveInput > -0.1f) ? AnimType.Idle : AnimType.Move;
    }

    private void ApplyJump()
    {
        if (inputs.Player.Jump.WasPressedThisFrame())
        {
            playerJump.Jump();
            //inputElement.animType = AnimType.Jump;
        }
        else if (inputs.Player.Jump.WasReleasedThisFrame())
        {
            playerJump.CancelJump();
        }
    }

    private void ApplyDash()
    {
        if (inputs.Player.Dash.WasPressedThisFrame())
        {
            playerDash.Dash();
        }
    }

    private void ApplyLight()
    {
        if (inputs.Player.Light.WasPressedThisFrame())
        {
            playerLight.Light();
        }
    }

    private enum AnimType
    {
        Idle, Move, Jump, Dash
    }

    private class InputElement
    {
        public float inputTime;
        public AnimType animType;
        public Vector2 playerPosition;
    }
}
