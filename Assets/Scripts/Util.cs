using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Quaternion LookAt2D(Vector3 obsPos, Vector3 targetPos)
    {
        Vector3 diff = targetPos - obsPos;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public static Quaternion UnitVectorToQuaternion(Vector2 inputVec)
    {
        return Quaternion.Euler(0f, 0f, (Mathf.Atan2(inputVec.y, inputVec.x) * Mathf.Rad2Deg) - 90f);
    }

    public static void MatchToTransform(Transform origTrans, Transform matchToTrans)
    {
        origTrans.position = matchToTrans.position;
        origTrans.rotation = matchToTrans.rotation;
    }
}
