using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Seek
{
    // list of waypoints
    public GameObject[] path;
    public int pathIndex = 0;
    public float targetThreshold = .1f;

    public override SteeringOutput getSteering()
    {
        // initialize target to first waypoint
        if (target == null)
        {
            target = path[pathIndex];
        }

        // increment path index when waypoint is reached
        if ((target.transform.position - character.transform.position).magnitude < targetThreshold)
        {
            pathIndex = (pathIndex + 1) % path.Length;
            target = path[pathIndex];
        }

        return base.getSteering();
    }

}
