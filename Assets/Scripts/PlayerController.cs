using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Range(0, 0.9f)] [SerializeField] private float _movementSpeed = .10f;
    [SerializeField] private float _JumpForce = 100f;
    
    [FormerlySerializedAs("this_rb")] public Rigidbody2D rb;
    public Collider2D[] groundColliders;
    public Collider2D[] waterColliders;

    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask waterLayer;
    
    public bool isGrounded;
    public bool isGroundedOnWater;
    public bool isControllable = true;
    public bool isAirControllable;
    public bool isFlipped = false;

    private Vector3 _velocity =  Vector3.zero;

    private const float BOUNDINGBOX_X = 0.65f;
    private const float BOUNDINGBOX_Y = 0.25f; 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    //Update call specifically for physics. Called before Update
    private void FixedUpdate()
    {
        if (isGrounded)
        {
            isControllable = true;
        }

        bool wasGrounded = isGroundedOnWater;
        isGrounded = false;
        isGroundedOnWater = false;
        
        // groundColliders = Physics2D.OverlapBoxAll()

    }

    
    public void Move(float move, bool jump) 
    {
        if (isControllable || isAirControllable)
        {
            //TODO: change 10f to be a specific value or 'const' it
            Vector3 targetVelocity = new  Vector2(move * 10f, rb.velocity.y);
            //Controls are flipped if we're in overflow
            if (isFlipped) targetVelocity *= -1;
            if (Input.GetAxis("Horizontal") != 0 || isGrounded && isControllable)
            {
                rb.velocity =
                    Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _velocity, _movementSpeed);
            }

            if (jump)
            {
                isGrounded = false;
                rb.AddForce(transform.localScale.y * _JumpForce * transform.up);
            }
        }
    }
    
}
