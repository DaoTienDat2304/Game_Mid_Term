using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPathAgent : MonoBehaviour
{
    public FlyPath flyPath;
    public float flySpeed = 5f;

    private int nextIndex = 1;

    void Start()
    {
        if (flyPath == null || flyPath.waypoints.Length == 0) return;

        transform.position = flyPath[0];
    }

    void Update()
    {
        if (flyPath == null || flyPath.waypoints.Length == 0) return;

        // ✅ Khi bay hết path → destroy enemy
        if (nextIndex >= flyPath.waypoints.Length)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 target = flyPath[nextIndex];

        if (Vector3.Distance(transform.position, target) > 0.01f)
        {
            FlyToNextWaypoint(target);
            LookAt(target);
        }
        else
        {
            nextIndex++;
        }
    }

    private void FlyToNextWaypoint(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            flySpeed * Time.deltaTime
        );
    }

    private void LookAt(Vector3 destination)
    {
        Vector2 position = transform.position;
        Vector2 target = destination;

        var lookDirection = target - position;
        if (lookDirection.magnitude < 0.01f) return;

        var angle = Vector2.SignedAngle(Vector2.down, lookDirection);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}