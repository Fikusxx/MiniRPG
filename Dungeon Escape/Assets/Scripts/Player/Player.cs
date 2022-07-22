using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    #region Player Inputs
    #endregion
    private float xInput;
    private float yInput;

    #region Movement
    [Space(10)]
    [Header("Movement")]
    #endregion
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedMultiplier = 1f;
    [SerializeField] private float jumpForce;

    #region Player States
    #endregion
    private bool isPlayerInputDisabled;
    private float lastSpriteDirection = 1f;
    private bool isGrounded;
    private bool isAttacking;
    [SerializeField] private float attackCooldown;

    #region References
    [Space(10)]
    [Header("References")]
    #endregion
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        GetPlayerInput();
        IsGrounded();

        Jump();
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }


    /// <summary>
    /// Get player's inputs
    /// </summary>
    private void GetPlayerInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// Attack
    /// </summary>
    private void Attack()
    {
        // if we press left click and we're not currently attacking
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            isAttacking = true;
            playerAnimation.ProcessAttack();

            StartCoroutine(DelayBetweenAttacks(attackCooldown));
        }
    }

    /// <summary>
    /// Set a delay between attacks
    /// </summary>
    private IEnumerator DelayBetweenAttacks(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        isAttacking = false;
    }

    /// <summary>
    /// Disable player input and set velocities to Vector2.zero
    /// </summary>
    private void DisablePlayerMovement()
    {
        isPlayerInputDisabled = true;
        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Move the player
    /// </summary>
    private void Move()
    {
        if (isPlayerInputDisabled == false)
        {
            // Handle movement and sprite direction
            rb.velocity = new Vector2(xInput * moveSpeed * moveSpeedMultiplier, rb.velocity.y);
            FlipSprite();

            // Switch between Run and Idle animations
            playerAnimation.ProcessHorizontalMovement(xInput);
        }
        
        // Process jumping logic
        playerAnimation.ProcessJumping(isGrounded);
    }

    /// <summary>
    /// If KeyCode.Space is pressed and the player isGrounded - jump
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 jump = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jump;
        }
    }

    /// <summary>
    /// Check if the player is grounded by casting a ray. Return false if he's not
    /// </summary>
    public bool IsGrounded()
    {
        //// Define isGrounded bool
        //bool isGrounded = false;

        // Cast a ray from transform.position of the player,
        // straight down, distance of 1f, affecting only Ground Layer
        var raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayer);

        // Draw that ray, so we can see it for 3f seconds
        //Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.yellow, 3f);

        // Since we only check for Ground layer, collider being != null works for us
        if (raycastHit.collider != null)
        {
            // Show name of the object ray collided with
            Debug.Log(raycastHit.collider.name);

            isGrounded = true;
            return isGrounded;
        }

        isGrounded = false;
        return isGrounded;
    }

    /// <summary>
    /// Flip the sprite according to moving direction. If player is not moving left/right, it will keep the last assigned value
    /// </summary>
    private void FlipSprite()
    {
        if (xInput > 0)
        {
            lastSpriteDirection = 1f;
            transform.localScale = new Vector2(lastSpriteDirection, 1f);
        }
        else if (xInput < 0)
        {
            lastSpriteDirection = -1f;
            transform.localScale = new Vector2(lastSpriteDirection, 1f);
        }
        else
        {
            transform.localScale = new Vector2(lastSpriteDirection, 1f);
        }
    }
}
