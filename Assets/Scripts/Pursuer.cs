using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Kinematic
{
    Pursue myMoveType;
    Face myPursueRotateType;
    LookWhereGoing myEvadeRotateType;

    // true means pursue, false means evade
    public bool flee = false;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = myTarget;
        myMoveType.flee = flee;

        myPursueRotateType = new Face();
        myPursueRotateType.character = this;
        myPursueRotateType.target = myTarget;

        myEvadeRotateType = new LookWhereGoing();
        myEvadeRotateType.character = this;
        myEvadeRotateType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = flee ? myEvadeRotateType.getSteering().angular : myPursueRotateType.getSteering().angular;
        base.Update();
    }
}
