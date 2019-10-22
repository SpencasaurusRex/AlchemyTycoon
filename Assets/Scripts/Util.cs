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

    public static Vector3 Round(this Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }

    public static Vector3 Round(this Vector3 v, Vector3 gridOffset)
    {
        return new Vector3(
            Mathf.Round(v.x - gridOffset.x) + gridOffset.x,
            Mathf.Round(v.y - gridOffset.y) + gridOffset.y,
            Mathf.Round(v.z - gridOffset.z) + gridOffset.z);
    }
}