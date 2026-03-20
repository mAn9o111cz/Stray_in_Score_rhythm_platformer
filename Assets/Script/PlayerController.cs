using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private Vector3 respawnPosition;
    private bool wasOnPlatform = false;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isMoving;
    private bool previousMoveState = false;
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position; 
    }

    void Update()
    {
        
        float moveInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Blend", Mathf.Abs(moveInput));
        if (moveInput != 0)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            isMoving = true;
            if (moveInput > 0) transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            if (moveInput < 0) transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        } else
        {
            isMoving = false;
        }

        if (previousMoveState == true && isMoving == false)
        {
            rb.linearVelocity = Vector3.zero;
        }

        previousMoveState = isMoving;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);
      
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    //void FixedUpdate()
    //{
    //    // 当从平台跳起时保持水平速度
    //    if (wasOnPlatform && transform.parent == null)
    //    {
    //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
    //    }

    //    wasOnPlatform = transform.parent != null;
    //}
    public void Die()
    {
        // 播放死亡动画/音效
        Debug.Log("Player died!");

        // 重置玩家位置
        Respawn();
    }

    public void Respawn()
    {
        // 重置位置到关卡起点
        transform.position = respawnPosition;

        // 重置玩家状态（如生命值、动画等）
        // ...
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void SetRespawnPosition(Vector3 newPosition)
    {
        respawnPosition = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlatformController pc = other.GetComponent<PlatformController>();
        if (pc != null)
        {
            transform.SetParent(pc.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlatformController pc = collision.GetComponent<PlatformController>();
        if (pc != null)
        {
            transform.SetParent(null);
        }
    }
}











