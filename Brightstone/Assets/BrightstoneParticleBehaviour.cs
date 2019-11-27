using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BrightstoneParticleBehaviour : MonoBehaviour
{
    private ParticleSystem deathParticles;
    private ParticleSystem.Particle[] auxParticles;
    private GameObject player;
    private int particleCount;
    [SerializeField] float movementWaitTime = 1.0f;
    private float timer = 0.0f;
    void Start()
    {
        player = GameManager.Instance.playerMovement.gameObject;
        deathParticles = GetComponent<ParticleSystem>();
        particleCount = deathParticles.particleCount;
        auxParticles = new ParticleSystem.Particle[particleCount];

        EnemyStats eS = GetComponentInParent<EnemyStats>();
        eS.OnDeath.AddListener(ActivateParticles);
    }

    // Update is called once per frame
    void Update()
    {
        if(deathParticles.isPlaying){
            if(timer < movementWaitTime){
                timer += Time.deltaTime;
            } else {
                particleCount = deathParticles.GetParticles(auxParticles);
                for (int i = 0; i < particleCount; i++){
                    ParticleSystem.Particle particle = auxParticles[i];
        
                    Vector3 particlePosition = deathParticles.transform.TransformPoint(particle.position);
                    Vector3 playerPosition = player.transform.position;
        
                    Vector3 targetPosition = (playerPosition - particlePosition) *  (particle.remainingLifetime / particle.startLifetime);
                    particle.position = deathParticles.transform.InverseTransformPoint(playerPosition - targetPosition);
                    auxParticles[i] = particle;
                }
                deathParticles.SetParticles(auxParticles, particleCount);
            }
        }
    }

    public void ActivateParticles(){
        deathParticles.Play();
    }
}
