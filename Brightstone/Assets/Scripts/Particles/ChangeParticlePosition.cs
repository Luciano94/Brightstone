using UnityEngine;

public class ChangeParticlePosition : MonoBehaviour
{
    ParticleLauncher pl;

    private void Start()
    {
        GameObject go = GameObject.Find("ParticleLauncher");

        if (!go)
        {
            enabled = false;
            return;
        }
        
        pl = go.GetComponent<ParticleLauncher>();
    }

    public void Splash()
    {
        Vector3 extraPos = new Vector3(Random.Range(-0.8f, 0.8f) + 0.0f,
                                       Random.Range(-0.8f, 0.8f) + 0.0f,
                                       -1.0f);
        pl.transform.position = transform.position + extraPos;
        pl.Killed();
    }
}