using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData data;

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerLight playerLight;

    private PlayerInputs inputs;
    private float moveInput;
    private AnimType currentAnimation;

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

        HandleAnimations();
    }

    private void ApplyMovement()
    {
        if (!playerDash.isDashing)
        {
            moveInput = inputs.Player.Move.ReadValue<float>();
            playerMovement.Move(moveInput);
        }

        //inputElement.animType = (moveInput < 0.1f && moveInput > -0.1f) ? AnimType.Idle : AnimType.Move;
    }

    private void ApplyJump()
    {
        playerJump.RefreshJump();
        
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
        if (playerJump.isGrounded)
        {
            playerDash.RefreshDash();
        }
        
        if (inputs.Player.Dash.WasPressedThisFrame() && !playerDash.isDashing)
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

    private void HandleAnimations()
    {
        if (playerDash.isDashing)
        {
            ChangeAnimationState(AnimType.Dash);
        }
        else if (playerJump.isJumping)
        {
            ChangeAnimationState(AnimType.Jump);
        }
        else if (playerJump.isFalling)
        {
            ChangeAnimationState(AnimType.Fall);
        }
        else
        {
            ChangeAnimationState(Mathf.Abs(moveInput) > 0 ? AnimType.Move : AnimType.Idle);
        }
    }

    private void ChangeAnimationState(AnimType animType)
    {
        if (animType == currentAnimation) return;

        animator.Play(animType.ToString());
        currentAnimation = animType;
    }

    private enum AnimType
    {
        Idle, Move, Jump, Fall,  Dash
    }

    private class InputElement
    {
        public float inputTime;
        public AnimType animType;
        public Vector2 playerPosition;
    }
}
