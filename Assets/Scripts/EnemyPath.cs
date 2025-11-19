using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] patrolPoints;
    public int startPoint, endPoint;
    public int currentPoint;
    
    public float movementSpeed;

    public bool smoothMovement;
    public bool backAndForth;
    private bool goBack;

    private float distToNext;
    private float distFromLast;
    private float speedMod;

    public bool randomized = true;

    public SpriteRenderer body;
    void Start()
    {
        if (randomized) currentPoint = Random.Range(0, patrolPoints.Length);
        transform.position = patrolPoints[currentPoint].position;
        
    }

    void FixedUpdate()
    {
        //We set the enemy to move to the next point
        distToNext = Vector2.Distance(transform.position, patrolPoints[currentPoint].position);
        if (currentPoint == 0)
        {
            distFromLast = Vector2.Distance(transform.position, patrolPoints[patrolPoints.Length - 1].position);
        }
        else
        {
            distFromLast = Vector2.Distance(transform.position, patrolPoints[currentPoint-1].position);
        }

        //If smoothMovement set, we smoothly move to the next point
        if (smoothMovement == true)
        {
            if (distToNext > distFromLast)
            {
                speedMod = distFromLast;
            }
            else
            {
                speedMod = distToNext;
            }

            if (speedMod > 1)
            {
                speedMod = 1;
            }
            transform.position = Vector2.MoveTowards(
                transform.position, 
                patrolPoints[currentPoint].position, 
                movementSpeed * speedMod * speedMod + 0.01f);
        }
        else //Otherwise, we rigidly move to the next point instead.
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                patrolPoints[currentPoint].position,
                movementSpeed
            );
        }

        if (transform.position == patrolPoints[currentPoint].position)
        {
            if (currentPoint != endPoint)
            {
                if (goBack) currentPoint--;
                else currentPoint++;
            }
        }
    
        //Logic for when we reach the end of a path
        if (currentPoint >= patrolPoints.Length)
        {
            if (backAndForth)
            {
                goBack = true;
                currentPoint = patrolPoints.Length - 1;
            }
            else currentPoint = 0;
        }
        
        //When we reach the end, we set goBack false to start the path again
        if (currentPoint == 0)
        {
            goBack = false;
        }
    }
    
    
    //(function lifted online for debugging purposes)
    private void OnDrawGizmos()
    {
        //Draw lines between each point
        Gizmos.color = Color.yellow;
        for(int i = 0; i < patrolPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    

}
