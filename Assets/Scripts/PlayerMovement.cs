using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float runSpeed;
    public float dashSpeed;
    public float default_grav = 3;
    private float moveSpeed;

    public int chargeTimer;
    public int maxChargeTime;
    public bool isCharging;


    private float horizontalMove = 0f;
    
    private bool jump;
    private int jumpTime = 0;

    public float drag = 0.35f;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public int i_frames = 0;
    [SerializeField] public int max_i_frames = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        moveSpeed = runSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = (2 * drag) + dashSpeed;
        horizontalMove = (Input.GetAxisRaw("Horizontal") * moveSpeed) - drag;
        
        
        //Jump Press: Jump & init charge
        if (Input.GetButtonDown("Jump"))
        {
            if (controller.isGrounded)
            {
                print("Jumping!");
                controller.rb.velocity = new Vector2(controller.rb.velocity.x, 0f);
                jump = true;
                jumpTime = 6;
                //coyoteTime = 0
            }
            else if (chargeTimer > 0)
            { 
                isCharging = true;
            }
        }
        
        //Jump Release: Release charge
        if (isCharging && Input.GetButtonUp("Jump"))
        {
            isCharging = false;
            chargeTimer = 0; 
            //TODO: Implement Charge Attack
        }
        
        //Reset charge if grounded 
        if (!isCharging && controller.isGrounded)
        {
            chargeTimer = maxChargeTime;
        }
        
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove, jump);
        jump = false;

        if (jumpTime > 0) jumpTime--;
        else jumpTime = 0;

        
        //If we're charging, turn off gravity
        if (isCharging)
        {
            controller.rb.gravityScale = 0;
            controller.rb.velocity = new Vector2(controller.rb.velocity.x, 0f);

            if (chargeTimer > 0) chargeTimer--;
            else
            {
                isCharging = false;
            }
        }
        else
        {
            controller.rb.gravityScale = default_grav;
        }
        //Do a jump if we have one 'queued'. Otherwise, reset jump timer
        if (!Input.GetButton("Jump"))
        {
            if (jumpTime > 0) controller.rb.AddForce(transform.localScale.y * 300 * -Vector2.up);
            jumpTime = 0;
        }
        
        //iFrames flickering 
        if (i_frames > 0)
        {
            if (i_frames % 2 == 0)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
            i_frames--;
        }
        else
        {
            spriteRenderer.enabled = true;
            i_frames = 0;
        }
    }
}
