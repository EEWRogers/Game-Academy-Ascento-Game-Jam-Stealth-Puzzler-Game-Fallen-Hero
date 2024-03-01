using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] float waypointSize = 0.1f;

    void OnDrawGizmos() 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int nextWaypoint = GetNextWaypoint(i);

            Gizmos.DrawSphere(GetWaypointPosition(i), waypointSize);
            Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(nextWaypoint));
        }
    }

    public int GetNextWaypoint(int i)
    {
        if (i == transform.childCount - 1)
        {
            return 0;
        }

        return i + 1;
    }

    public Vector3 GetWaypointPosition(int i)
    {
        return transform.GetChild(i).position;
    }
}
