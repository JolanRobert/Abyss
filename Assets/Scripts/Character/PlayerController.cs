using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerData data;

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerDash playerDash;
    [SerializeField] private PlayerLight playerLight;

    [SerializeField] private int inputBufferSize;

    private PlayerInputs inputs;
    private float moveInput;
    private AnimType currentAnimation;

    private bool isDashEnabled;
    private bool isLightEnabled;

    public InputElement[] inputBuffer;
    private InputElement inputElement = new();
    private int inputBufferIdx = 0;

    private Vector2 checkPoint;
    [SerializeField] private Image blackScreen;
    [SerializeField] private float transitionTime;

    private void Start()
    {
        isDashEnabled = false;
        isLightEnabled = false;
        data.nbJump = 1;

        checkPoint = new Vector2(transform.position.x, transform.position.y);

        inputElement.inputTime = Time.time;
        inputElement.animType = AnimType.Idle;
        inputElement.playerPosition = transform.position;

        inputBuffer = new InputElement[inputBufferSize];
        for (int i = 0; i < inputBufferSize; i++) 
        {
            inputBuffer[i] = inputElement;
        }

        inputs = new PlayerInputs();
        inputs.Enable();
    }

    private void Update()
    {
        inputElement = new();
        inputElement.inputTime = Time.time;
        inputElement.playerPosition = transform.position;

        ApplyMovement();
        ApplyJump();
        ApplyDash();
        ApplyLight();

        HandleAnimations();

        BufferInput();
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
        
        if (inputs.Player.Dash.WasPressedThisFrame() && !playerDash.isDashing && isDashEnabled)
        {
            playerDash.Dash();
        }
    }

    private void ApplyLight()
    {
        if (inputs.Player.Light.WasPressedThisFrame() && isLightEnabled)
        {
            playerLight.Light();
        }
    }

    private void HandleAnimations()
    {
        if (playerDash.isDashing)
        {
            ChangeAnimationState(AnimType.Dash);
            inputElement.animType = AnimType.Dash;
        }
        else if (playerJump.isJumping)
        {
            ChangeAnimationState(AnimType.Jump);
            inputElement.animType = AnimType.Jump;
        }
        else if (playerJump.isFalling)
        {
            ChangeAnimationState(AnimType.Fall);
            inputElement.animType = AnimType.Fall;
        }
        else
        {
            ChangeAnimationState(Mathf.Abs(moveInput) > 0 ? AnimType.Move : AnimType.Idle);
            inputElement.animType = Mathf.Abs(moveInput) > 0 ? AnimType.Move : AnimType.Idle;
        }
    }

    private void ChangeAnimationState(AnimType animType)
    {
        if (animType == currentAnimation) return;

        animator.Play(animType.ToString());
        currentAnimation = animType;
    }

    private void BufferInput() 
    {
        inputBuffer[inputBufferIdx] = inputElement;
        inputBufferIdx = (++inputBufferIdx % inputBufferSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ShadowController shadow) && shadow.canHurt)
        {
            StartCoroutine(BlackScreenCoroutine(transitionTime));
            shadow.canHurt = false;
        }
    }

    private void Respawn() 
    {
        transform.position = checkPoint;
    }

    private float fadeTime;
    IEnumerator BlackScreenCoroutine(float t) 
    {
        fadeTime = 0;
        inputs.Disable();
        while (fadeTime < t / 3) 
        {
            yield return new WaitForEndOfFrame();
            blackScreen.color = Color.Lerp(blackScreen.color, new Color(0, 0, 0, 1), 0.1f);
            fadeTime += Time.deltaTime;
        }
        blackScreen.color = new Color(0, 0, 0, 1);
        
        Respawn();
        yield return new WaitForSeconds(t / 3);

        inputs.Enable();
        fadeTime = 0;

        while (fadeTime < t / 3)
        {
            yield return new WaitForEndOfFrame();
            if (fadeTime < t / 6) 

            blackScreen.color = Color.Lerp(blackScreen.color, new Color(0, 0, 0, 0), 0.01f);
            fadeTime += Time.deltaTime;
        }
        

        blackScreen.color = new Color(0, 0, 0, 0);
    }

    public void SetNbJump(int nbJump) 
    {
        data.nbJump = nbJump;
    }

    public void EnableDash(bool enabled) 
    {
        isDashEnabled = enabled;
    }

    public void EnableLight(bool enabled) 
    {
        isLightEnabled = enabled;
    }

    public void SetCheckPoint(Vector2 pos) 
    {
        checkPoint = pos;
    }


    public enum AnimType
    {
        Idle, Move, Jump, Fall,  Dash
    }

    public class InputElement
    {
        public float inputTime;
        public AnimType animType;
        public Vector2 playerPosition;
    }
}
