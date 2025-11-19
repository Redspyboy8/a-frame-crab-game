using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RailObjSpawner : MonoBehaviour
{
    public Transform spawnRef;

    public Transform spawnPoint;

    public Transform levelGrid;

    public float startX;

    public float obs_distance = 60;

    public bool isRunning = true;

    [SerializeField] public LevelData level;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x + obs_distance;
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRef.position.x <= 0)
        {
            if (isRunning)
            {
                spawnRef.position = new Vector3(startX, spawnRef.position.y, 0);
                CreateObstacle();
            }
            print("Updating Position!");
        }
    }

    private void CreateObstacle()
    {
        print("Creating obstacle");
        if (level == null) return;
        Vector3 newPos = new Vector3(
            spawnRef.position.x - Random.Range(0, 4),
            spawnPoint.position.y,
            0
        );
        GameObject obj = Instantiate(level.obstacles[Random.Range(0, level.obstacles.Count)], newPos, spawnRef.rotation);
        obj.transform.SetParent(levelGrid);
    }
}
