using UnityEngine;

public static class Calculations{
    public static Axis8Direction Get8AxisDirection(Vector3 distance){
        float angle = Vector3.SignedAngle(distance, Vector3.up, Vector3.forward);
        
        if      (angle >  -22.5f && angle <=   22.5f)
            return Axis8Direction.Up;
        else if (angle >   22.5f && angle <=   67.5f)
            return Axis8Direction.UpRight;
        else if (angle >   67.5f && angle <=  112.5f)
            return Axis8Direction.Right;
        else if (angle >  112.5f && angle <=  157.5f)
            return Axis8Direction.DownRight;
        else if (angle >  157.5f || angle <= -157.5f)
            return Axis8Direction.Down;
        else if (angle > -157.5f && angle <= -112.5f)
            return Axis8Direction.DownLeft;
        else if (angle > -112.5f && angle <=  -67.5f)
            return Axis8Direction.Left;
        else if (angle >  -67.5f && angle <=  -22.5f)
            return Axis8Direction.UpLeft;

        return Axis8Direction.Down;
    }

    public static Axis4Direction Get4AxisDirection(Vector3 distance){
        float angle = Vector3.SignedAngle(distance, Vector3.up, Vector3.forward);
        
        if      (angle >  -45.0f && angle <=   45.0f)
            return Axis4Direction.Up;
        else if (angle >   45.0f && angle <=  135.0f)
            return Axis4Direction.Right;
        else if (angle >  135.0f || angle <= -135.0f)
            return Axis4Direction.Down;
        else if (angle > -135.0f && angle <=  -45.0f)
            return Axis4Direction.Left;

        return Axis4Direction.Down;
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
