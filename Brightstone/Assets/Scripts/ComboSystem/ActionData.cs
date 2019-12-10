public enum ActionState{
    enterFrames = 0,
    activeFrames,
    exitFrames,
}

public enum Actions{
    X = 0,
    Y,
    B,
    A,
    Blank,
}

public struct FrameData{
    public float enterFrames;
    public float activeFrames;
    public float exitFrames;
    public ActionState State;
}