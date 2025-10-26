using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    TouchingDirections touchingDirections;

    public float CurrentMoveSpeed{get{
        if(!IsMoving){
            return 0;
        }
        if(touchingDirections.IsGrounded){
            if(IsMoving){
                if(IsRunning){
                    return runSpeed;
                    }
                else{
                    return walkSpeed;
                }
            }
        }
        else{
            return airWalkSpeed;
        }

        return 0;
    }}
    
    Vector2 moveInput;
    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get{
        return _isMoving;
    } private set{
        _isMoving = value;
        animator.SetBool("isMoving", value);        

    } }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
            
        }
    }

    public bool _IsFacingRight = true;

    public bool IsFacingRight{get{return _IsFacingRight;}private set{
        if(_IsFacingRight != value){
            transform.localScale *= new Vector2(-1, 1);
        }

        _IsFacingRight = value;
    }}

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //move
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);

        animator.SetFloat("yVelocity", rb.linearVelocity.y); 

        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput){
        if(moveInput.x > 0 && !IsFacingRight){
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight){
            IsFacingRight = false;
        }

    }

    public void OnRun(InputAction.CallbackContext context){
        if(context.started){
            IsRunning = true;
        }
        else if(context.canceled){
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context){
        if(context.started && touchingDirections.IsGrounded){
            animator.SetTrigger("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }
    
}
