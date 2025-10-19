using UnityEngine;

public class JumpEnemyAttacker : MonoBehaviour
{
    private bool facingRight = true;
    private float moveDirection = 1;

    [Header("For JumpSettings")]
    [SerializeField] float jumpStrength = 5f;  
    [SerializeField] float horizontalStrength = 5f;

    
    [SerializeField] Transform player;
    [SerializeField] float range;
    
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 boxSize;
    
    private bool isGrounded;
    private Rigidbody2D enemyRB;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     enemyRB = GetComponent<Rigidbody2D>(); 
     animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        AnimationController();
        if(isGrounded && PlayerInSight()){
            JumpAttack();
        }
    }
            


    bool PlayerInSight(){
        
        return Mathf.Abs(player.position.x - transform.position.x) < range;
        
    }

    void JumpAttack(){
        
        if(isGrounded){
            
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        enemyRB.linearVelocity = new Vector2(direction * horizontalStrength, jumpStrength);
    }
    }

    
    void AnimationController(){
        animator.SetBool("PlayerInSight", PlayerInSight());
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheck.position, boxSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }
}

