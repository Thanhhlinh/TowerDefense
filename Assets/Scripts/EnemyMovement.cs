using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;
    // Start is called before the first frame update
    private void Start()
    {
        target = WayPoint.points[wavepointIndex];
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= WayPoint.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = WayPoint.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerState.Lives--;
        WaveSpawner.EnemiesAlive--;
        DestroyImmediate(gameObject);
    }
}
