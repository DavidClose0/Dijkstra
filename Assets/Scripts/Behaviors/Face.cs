using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    // override Align's getTargetAngle to face the target instead of matching its orientation
    public override float getTargetAngle()
    {
        float targetAngle;
        Vector3 direction = target.transform.position - character.transform.position;

        // if direction vector is zero, make no change
        if (direction.magnitude == 0)
        {
            return character.transform.eulerAngles.y;
        }

        // calculate angle to target and convert to degrees
        targetAngle = Mathf.Atan2(direction.x, direction.z);
        targetAngle *= Mathf.Rad2Deg;

        return targetAngle;
    }
}
