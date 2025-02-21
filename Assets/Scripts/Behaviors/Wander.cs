using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Wander : Seek
{
    // radius and forward offset of the wander circle
    float wanderOffset = 1f;
    float wanderRadius = 1f;

    // maximum rate at which the wander orientation can change
    public float wanderRate = 10f;

    // current orientation of the wander target
    float wanderOrientation = 0f;

    // maximum acceleration of the character
    float maxAcceleration = 100f;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // update wander orientation
        wanderOrientation += Random.Range(-1f, 1f) * wanderRate;

        // calculate combined target orientation
        float targetOrientation = wanderOrientation + character.transform.eulerAngles.y;

        // calculate the center of the wander circle
        Vector3 targetPosition = character.transform.position + wanderOffset * new Vector3(Mathf.Sin(character.transform.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Cos(character.transform.eulerAngles.y * Mathf.Deg2Rad));

        // calculate the target location.
        targetPosition += wanderRadius * new Vector3(Mathf.Sin(targetOrientation * Mathf.Deg2Rad), 0, Mathf.Cos(targetOrientation * Mathf.Deg2Rad));
        
        // Get the direction to the target
        result.linear = targetPosition - character.transform.position;

        // give full acceleration along this direction
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}