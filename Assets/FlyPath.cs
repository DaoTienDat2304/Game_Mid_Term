using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPath : MonoBehaviour
{
    public Waypoint[] waypoints;

    private void Reset() => waypoints = GetComponentsInChildren<Waypoint>();
}