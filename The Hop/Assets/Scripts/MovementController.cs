using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    #region MOVMENT
    public Rigidbody2D rb;
    private float dirX;
    public float runMaxSpeed = 10f;
    public float runAccel = 10f;
    public float runDeccel = 12f;
    #endregion

    #region JUMP
    private float jumpForce = 15f;
    #endregion

    #region GROUDCHECK
    private Vector2 groundCheckSize = new Vector2(1f, 0.03f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    #endregion

    #region ANIMATOR
    public Animator animator;
    private string animationJumpName = "Jump";
    private string animationFallName = "Fall";
    private string animationDeathName = "Death";
    #endregion

    void Awake()
    {
        GameController.onGameOver += PlayerDeath;
    }
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        // Here is line that provides rotation phone input
        // dirX = Input.acceleration.x;
    }
    void FixedUpdate()
    {
        if (GameController.gameController.GetGameState() != GameState.GS_GAME_OVER)
        {
            Jump();
            Run();
            SetProperFrames();
        }
        else
        {
            FreezePlayer();
        }
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void SetProperFrames()
    {
        if (rb.velocity.y > 0)
        {
            float normalizedTime = Mathf.Clamp01(1 - Math.Abs(rb.velocity.y) / jumpForce);
            animator.Play(animationJumpName, 0, normalizedTime);
        }
        else
        {
            float normalizedTime = Mathf.Clamp01(Math.Abs(rb.velocity.y) / jumpForce);
            animator.Play(animationFallName, 0, normalizedTime);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
    }
    private void Run()
    {
        float targetSpeed = dirX * runMaxSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float acceleration = (Math.Abs(targetSpeed) > 0.01f) ? runAccel : runDeccel;
        float forceMomuentum = speedDif * acceleration;
        rb.AddForce(forceMomuentum * Vector2.right, ForceMode2D.Force);

    }
    void PlayerDeath()
    {
        animator.Play(animationDeathName);
    }

    void OnDethAnimDestroyPlayer()
    {
        Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        GameController.onGameOver -= PlayerDeath;
    }
    private void FreezePlayer()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }
}
