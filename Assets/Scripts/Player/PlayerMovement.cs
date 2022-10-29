using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    static public PlayerMovement instance;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float moveSpeed = 250f;
    private float climbSpeed = 150f;
    private float jumpForce = 500f;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 velocity = Vector3.zero;
    public bool isGrounded = true;
    public bool isClimbing = false;
    private bool isJumping;
    private bool isAllowedToMove = true;

    private Animator animator;

    public BoxCollider2D groundCheck;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        groundCheck = transform.Find("GroundCheck").GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            animator.SetTrigger("jump");
        }

        Flip(rb.velocity.x);

        float characterVelocityX = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("SpeedX", characterVelocityX);
        float characterVelocityY = rb.velocity.y;
        animator.SetFloat("SpeedY", characterVelocityY);
    }

    void FixedUpdate()
    {
        if (isAllowedToMove)
        {
            MovePlayer(horizontalMovement, verticalMovement);
        }
    }


    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isGrounded", false);
            isGrounded = false;
        }
    }


    public void StopPlayer()
    {
        isAllowedToMove = false;
    }

    public void ResumePlayer()
    {
        isAllowedToMove = true;
    }

    public void ResetPlayer(Vector2 position)
    {
        transform.position = position;
    }

    public void StartFalling()
    {
        //animator.SetTrigger("fall");
    }

}