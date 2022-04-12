using UnityEngine;

/*
The purpose of this script is:
*/

public static class ExtensionMethods
{
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static void MyVelocity(this Rigidbody2D original, float? x = null, float? y = null)
    {
        original.velocity = new Vector2(x ?? original.velocity.x, y ?? original.velocity.y);
    }

    public static bool IsBetween(float max, float min, float compare)
    {
        return max >= compare && compare >= min;
    }

    public static int RandomSign()
    {
        int number = Random.Range(0, 2);
        return number == 0 ? 1 : -1;
    }
}
