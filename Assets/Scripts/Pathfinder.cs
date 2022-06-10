using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    EnemySpawner enemySpawner;
    WaveConfig waveConfig;
    
    List<Transform> waypoints;
    int wayPointIndex = 0; 

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    
    void Start()
    {
        waveConfig = enemySpawner.GetCurerntWave();

        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[wayPointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (wayPointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[wayPointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            
            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
