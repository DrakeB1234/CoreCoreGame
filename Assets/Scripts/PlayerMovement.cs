using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jetpackForce;
    [SerializeField]
    private float jetpackTime;

    [SerializeField] 
    private LayerMask groundLayer;
    [SerializeField] 
    private Transform groundPos;
    [SerializeField]
    private Animator _CharacterAnimator;

    private Rigidbody2D _PlayerRB;
    private float horizontalInput;
    private bool isFacingRight;
    private bool isJetpackHold;
    private float currentJetpackTime;

    private void Awake() 
    {
        _PlayerRB = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        _PlayerRB.velocity = new Vector2(horizontalInput * playerSpeed, _PlayerRB.velocity.y);

        // Check if player jetpack is activated (activates if player jumps while airborne)
        if (isJetpackHold && currentJetpackTime > 0)
        {
            _PlayerRB.velocity = new Vector2(_PlayerRB.velocity.x, jetpackForce);
            currentJetpackTime -= Time.deltaTime;
        }
        else
        {
            // Set animator bool
            _CharacterAnimator.SetBool("isJetpackTime", false);
        }

        // Check if player is grounded
        if (_PlayerRB.velocity.y == 0)
        {
            // Set animator bool
            _CharacterAnimator.SetBool("isJumping", false);
        }

        // Check if player is falling and animator bool is false
        if (_PlayerRB.velocity.y < 0 && !_CharacterAnimator.GetBool("isJumping"))
        {
            // Set animator trigger fall
            _CharacterAnimator.SetTrigger("fallTrigger");
        }
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<Vector2>().x;
        horizontalInput = val;

        // Set animator to walk animation
        _CharacterAnimator.SetBool("isRunning", true);

        // Flip character according to input value
        if (!isFacingRight && val < 0)
            FlipPlayer();
        else if (isFacingRight && val > 0)
            FlipPlayer();
        // Value is 0, set animator to idle animation
        else if (val == 0)
            _CharacterAnimator.SetBool("isRunning", false);
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        // Jump button pressed and is grounded (first jump)
        if (context.performed && IsGrounded())
        {
            // Reset jetpack time
            currentJetpackTime = jetpackTime;

            // Set animator bool
            _CharacterAnimator.SetBool("isJumping", true);
            // Set animator to trigger jump animation
            _CharacterAnimator.SetTrigger("jumpTrigger");

            _PlayerRB.velocity = new Vector2(_PlayerRB.velocity.x, jumpForce);
            return;
        }
        // Jump button pressed after already in air (jetpack)
        else if (context.performed)
        {
            isJetpackHold = true;

            // Set animator bool
            _CharacterAnimator.SetBool("isJetpackTime", true);
            // Set animator to trigger jetpack animation
            _CharacterAnimator.SetTrigger("jetpackTrigger");
        }
        else if (context.canceled)
        {
            // Set animator bool
            _CharacterAnimator.SetBool("isJetpackTime", false);

            isJetpackHold = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundPos.position, 0.2f, groundLayer);
    }

    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
