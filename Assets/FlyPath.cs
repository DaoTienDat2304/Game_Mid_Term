using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPath : MonoBehaviour
{
    public Waypoint[] waypoints;

    // Tự động lấy waypoint con khi bấm Reset trong Inspector
    private void Reset()
    {
        waypoints = GetComponentsInChildren<Waypoint>();
    }

    // INDEXER: cho phép truy cập position kiểu flyPath[i]
    public Vector3 this[int index] => waypoints[index].transform.position;

    // Vẽ đường trong Scene view
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] == null || waypoints[i + 1] == null) continue;

            Gizmos.DrawLine(
                waypoints[i].transform.position,
                waypoints[i + 1].transform.position
            );
        }
    }
}