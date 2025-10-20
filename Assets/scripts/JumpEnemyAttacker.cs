using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class JumpEnemyAttacker : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player; // drag the Player here, or find by tag in Awake

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;   // empty child under the feet
    [SerializeField] private Vector2 groundBoxSize = new Vector2(0.8f, 0.1f);
    [SerializeField] private LayerMask groundLayer = ~0; // set to your Ground layer

    [Header("Aggro")]
    [SerializeField] private float detectRadius = 15f;

    [Header("Jump")]
    [SerializeField] private float jumpImpulseY = 8f;
    [SerializeField] private float jumpImpulseX = 4f; // horizontal push toward player
    [SerializeField] private float jumpCooldown = 0.4f;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCol;

    private bool facingRight = true;
    private bool isGrounded;
    private float nextJumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (groundCheck == null)
        {
            // fallback: use collider bottom as groundCheck if not provided
            groundCheck = new GameObject("GroundCheck").transform;
            groundCheck.SetParent(transform);
            var b = boxCol.bounds;
            groundCheck.position = new Vector2(b.center.x, b.min.y - 0.02f);
        }
    }

    private void FixedUpdate()
    {
        // 1) Ground check
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);

        // 2) Player detection (null-safe)
        bool playerSeen = false;
        if (player != null)
        {
            float dist = Vector2.Distance(player.position, transform.position);
            playerSeen = dist <= detectRadius;

            // Face the player smoothly (flip once)
            float deltaX = player.position.x - transform.position.x;
            if (deltaX < 0f && facingRight) Flip();
            else if (deltaX > 0f && !facingRight) Flip();

            // 3) Jump if close & grounded & off cooldown
            if (playerSeen && isGrounded && Time.time >= nextJumpTime)
            {
                float dir = Mathf.Sign(deltaX); // -1 left, +1 right
                Vector2 impulse = new Vector2(dir * jumpImpulseX, jumpImpulseY);
                rb.AddForce(impulse, ForceMode2D.Impulse);
                nextJumpTime = Time.time + jumpCooldown;
            }
        }

        // 4) Animation params (single call)
        anim.SetBool("PlayerInSight", playerSeen);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x *= -1f;
        transform.localScale = s;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        }
        Gizmos.color = new Color(0f, 0.5f, 1f, 0.15f);
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}