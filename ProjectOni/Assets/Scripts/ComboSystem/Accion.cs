using UnityEngine;

public class Accion : ScriptableObject{
    struct FrameData{
        float EnterTime;
        float ActionTime;
        float ExitTime;
    }

    private FrameData fData;
    private bool isActive;
}
