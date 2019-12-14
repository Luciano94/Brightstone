using System.Collections.Generic;
using UnityEngine;

public class BrightstoneManager : MonoBehaviour{
    private static BrightstoneManager instance;

    public static BrightstoneManager Instance{
        get {
            instance = FindObjectOfType<BrightstoneManager>();
            if(instance == null){
                GameObject go = new GameObject("BrightstoneManager");
                instance = go.AddComponent<BrightstoneManager>();
            }
            return instance;
        }
    }

    [System.Serializable]
    public struct BrightstoneData{
        public string name;
        public Gradient color;
        public float sizeOnStart;
    }

    public struct ActiveBrightstone{
        public Transform transform;
        public BrightstoneTypes type;
    }

    public enum BrightstoneTypes{
        CombatBeatdown,
        CombatThrust,
        CombatShuriken,
        CombatZone,
        Experience,
        Count,
    }

    [SerializeField] private GameObject brightstoneParticlesGO;
    [SerializeField] private float movSpeed;
    [SerializeField] private float minDist;
    [SerializeField] private float givenExperience;
    [SerializeField] private BrightstoneData[] brightstonesData;

    private HashSet<ActiveBrightstone> particles = new HashSet<ActiveBrightstone>();
    private bool movingToPlayer = false;
    private GameManager gM;

    private void Start(){
        gM = GameManager.Instance;
    }

    private void Update(){
        if (!movingToPlayer && gM.activeRoom.GetRoomsBehaviour().Complete && particles.Count > 0){
            movingToPlayer = true;
            StopYAxisMovement();
        }

        if (movingToPlayer)
            MakeParticlesMovement();
    }

    private void StopYAxisMovement(){
        foreach (ActiveBrightstone particle in particles){
            ParticleSystem.VelocityOverLifetimeModule lOLTM = particle.transform.GetComponent<ParticleSystem>().velocityOverLifetime;
            lOLTM.enabled = false;
        }
    }

    private void MakeParticlesMovement(){
        foreach (ActiveBrightstone particle in particles){
            Vector3 diff = gM.PlayerPos - particle.transform.position;

            particle.transform.position += diff.normalized * movSpeed * Time.deltaTime;

            if (diff.magnitude < minDist){
                particle.transform.GetComponent<ParticleSystem>().Stop();
                particle.transform.GetComponent<Autodestroy>().enabled = true;
                particles.Remove(particle);
                
                switch(particle.type){
                    case BrightstoneTypes.Experience:
                        GameManager.Instance.playerSts.AddExperience(givenExperience);
                        
                    break;

                    default:

                    break;
                }

                if (particles.Count == 0)
                    movingToPlayer = false;

                return;

                // Here i have to make a call to someone telling to:
                //  - Increment experience.
                //  - Increment the combo of an attack.
            }
        }
    }

    public void OnEnemyDeath(EnemyType enemyType, Vector3 locationToSpawn){
        BrightstoneTypes bType;

        if ((int)enemyType <= 3)
            bType = BrightstoneTypes.Experience;
        else{
            switch (enemyType){
                case EnemyType.Boss:
                bType = BrightstoneTypes.CombatBeatdown;
                break;

                default:
                bType = BrightstoneTypes.CombatBeatdown;
                break;
            }
        }

        GameObject particle = Instantiate(brightstoneParticlesGO, locationToSpawn, transform.rotation);

        particles.Add(new ActiveBrightstone() { transform = particle.transform, type = bType });
        var main = particle.GetComponent<ParticleSystem>().main;

        main.startSize = brightstonesData[(int)bType].sizeOnStart;
        main.startColor = brightstonesData[(int)bType].color;
    }
}