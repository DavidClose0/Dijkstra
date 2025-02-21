using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : Kinematic
{
    Separation myMoveType;
    LookWhereGoing myRotateType;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Separation();
        myMoveType.character = this;
        myMoveType.targets = new Kinematic[1];
        myMoveType.targets[0] = myTarget.GetComponent<Kinematic>();

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
