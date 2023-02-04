using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Vector3 XYToXZ(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    public static Vector2 XZToXY(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }
}
