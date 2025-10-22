using UnityEngine;

public class FixedEnemyJumper : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float detectRadiusRange;
    public float jumpCooldown;
    public float jumpImpulse;
    float lastJumpTime;
    //private bool isGrounded;
    private Rigidbody2D rb;
    EnemyTouchingDirections touchingDirections;

    /*[Header("Ground Check")]
    [SerializeField] private Transform groundCheck;   // empty child under the feet
    [SerializeField] private Vector2 groundBoxSize = new Vector2(0.8f, 0.1f);
    [SerializeField] private LayerMask groundLayer = ~0; // set to your Ground layer*/

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<EnemyTouchingDirections>();
    }


    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
        float dist = Vector2.Distance(player.transform.position, transform.position);
        Vector3 scale = transform.localScale;

        if(dist < detectRadiusRange){
        if(player.transform.position.x > transform.position.x){
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else{
            transform.Translate(speed * Time.deltaTime * -1, 0, 0);
        }

        transform.localScale = scale;
        
        //Debug.Log("isGrounded = " + isGrounded);

        if ( touchingDirections.IsGrounded && Time.time > lastJumpTime + jumpCooldown) {

                lastJumpTime = Time.time;

                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        }
        Gizmos.color = new Color(0f, 0.5f, 1f, 0.15f);
        Gizmos.DrawWireSphere(transform.position, detectRadiusRange);
    }*/
}
