
using UnityEngine;

public class ZoomWhenParrying : MonoBehaviour
{
    [SerializeField] float distanceToZoom;
    [SerializeField] float velocityTurningBack;
    [SerializeField] float minCameraSize;
    [SerializeField] float maxCameraSize;
    [SerializeField] float timeToGoBack;

    private float timeLeft;

    void Update()
    {
        if (Camera.main.orthographicSize < maxCameraSize)
        {
            if (timeLeft <= 0)
                Camera.main.orthographicSize += velocityTurningBack;
            else
                timeLeft -= Time.deltaTime;
        }
    }

    public void ReduceSize()
    {
        if(Camera.main.orthographicSize > minCameraSize)
            Camera.main.orthographicSize -= distanceToZoom;

        timeLeft = timeToGoBack;
    }

    public void SetNewMaxSize(float newSize)
    {
        maxCameraSize = newSize;
    }
}