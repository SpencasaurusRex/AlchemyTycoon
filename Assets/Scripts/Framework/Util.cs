using UnityEngine;

public static class Util
{ 
    public static Vector3 WithZ(this Vector3 v, float z)
    {
        v.z = z;
        return v;
    }

    public static Vector3 WithZ(this Vector2 v, float z)
    { 
        return new Vector3(v.x, v.y, z);
    }
}