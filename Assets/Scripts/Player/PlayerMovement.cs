using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    static public PlayerMovement instance;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float moveSpeed = 5f;
    private float jumpForce = 8f;
    public bool isGrounded = true;
    private bool isJumping;
    private bool isAllowedToMove = true;

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
        spriteRenderer = transform.Find("bonhomme").GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            isGrounded = false;
        }

        if (isAllowedToMove)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += Vector3.right * -moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
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


}