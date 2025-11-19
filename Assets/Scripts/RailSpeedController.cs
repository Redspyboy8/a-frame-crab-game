using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RailSpeedController : MonoBehaviour
{
    public float levelSpeed;
    private float baseSpeed;
    public Rigidbody2D rb;
    public Transform dist;

    public bool isGameRunning; 
    public PlayerMovement playerMovement;
    public float deltaSpeed;

    void Start()
    {
        rb.velocity = transform.right * levelSpeed * -1;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * levelSpeed * -1;
        if (playerMovement != null)
        {
            levelSpeed = baseSpeed + (-dist.position.x * deltaSpeed);
            playerMovement.drag = levelSpeed * 0.05f;
        }

    }

    public void SetSpeed(float newSpeed)
    {
        baseSpeed = newSpeed;
        levelSpeed = newSpeed;
    }
}
