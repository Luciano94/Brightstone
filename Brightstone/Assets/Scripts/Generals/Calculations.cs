using UnityEngine;

public static class Calculations{
    public static AxisDirection GetDirection(Vector3 distance){
        float angle = Vector3.SignedAngle(distance, Vector3.up, Vector3.forward);
        
        if      (angle >  -22.5f && angle <=   22.5f)
            return AxisDirection.Up;
        else if (angle >   22.5f && angle <=   67.5f)
            return AxisDirection.UpRight;
        else if (angle >   67.5f && angle <=  112.5f)
            return AxisDirection.Right;
        else if (angle >  112.5f && angle <=  157.5f)
            return AxisDirection.DownRight;
        else if (angle >  157.5f || angle <= -157.5f)
            return AxisDirection.Down;
        else if (angle > -157.5f && angle <= -112.5f)
            return AxisDirection.DownLeft;
        else if (angle > -112.5f && angle <=  -67.5f)
            return AxisDirection.Left;
        else if (angle >  -67.5f && angle <=  -22.5f)
            return AxisDirection.UpLeft;

        return AxisDirection.Down;
    }

    public static float GetAngle(Vector3 diff){
        float angle = Vector3.SignedAngle(diff, Vector3.up, Vector3.forward);

        if (angle >= 0.0f && angle <= 90.0f)
            return 90.0f - angle;
        else if (angle > 90.0f && angle <= 180.0f)
            return 450.0f - angle;
        else // if (angle < 0.0f && angle >= -180.0f)
            return 90.0f + Mathf.Abs(angle);
    }
}
