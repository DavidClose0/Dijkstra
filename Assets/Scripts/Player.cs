using System;
using UnityEngine;

public class Player : Kinematic
{
    public float maxLinearAcceleration = 10f;
    public float maxAngularAcceleration = 1000f;
    public float linearDrag = .95f;
    public float angularDrag = .95f;

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();

        if (Input.GetKey("d"))
        {
            steeringUpdate.angular = maxAngularAcceleration;
        }
        if (Input.GetKey("a"))
        {
            steeringUpdate.angular = -maxAngularAcceleration;
        }
        if (Input.GetKey("w"))
        {
            steeringUpdate.linear = maxLinearAcceleration * this.transform.forward;
        }
        if (Input.GetKey("s"))
        {
            steeringUpdate.linear = -maxLinearAcceleration * this.transform.forward;
        }

        // Apply drag
        linearVelocity *= Mathf.Pow(linearDrag, Time.deltaTime);
        angularVelocity *= Mathf.Pow(angularDrag, Time.deltaTime);

        base.Update();
    }
}
