using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startPos;
    public float maxDist;
    public float parallax;

    public float offset;
    public int loops;

    public RailObjSpawner rail;

    void Start()
    {
        if (rail == null)
        {
            rail = FindObjectOfType<RailObjSpawner>();
        }
        startPos = transform.position.x;
        loops = 0;
    }

    void FixedUpdate()
    {
        float dist = (rail.transform.position.x * parallax);
        transform.position = new Vector3(
            startPos + dist + ((maxDist + offset) * loops), 
            transform.position.y,
            transform.position.z
            );
        if (transform.position.x <= startPos - maxDist)
        {
            loops = -(int)dist / (int)maxDist;
        }
    }
}