using System.Collections;
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

    public enum BrightstoneTypes{
        CombatBeatdown,
        CombatThrust,
        CombatShuriken,
        CombatZone,
        Experience,
        Count,
    }

    [SerializeField] private GameObject brightstoneParticles;
    [SerializeField] private float movSpeed;
    [SerializeField] private float minDist;
    [SerializeField] private BrightstoneData[] brightstonesData;

    private List<Transform> particles = new List<Transform>();
    private GameManager gM;

    private void Start(){
        gM = GameManager.Instance;
    }

    private void Update(){
        //if (gM.activeRoom.GetRoomsBehaviour().Complete)
        //    MakeParticlesMovement();
    }

    private void MakeParticlesMovement(){
        foreach (Transform particle in particles){
            Vector3 diff = particle.position - gM.PlayerPos;

            particle.position += diff.normalized * movSpeed * Time.deltaTime;

            if (diff.magnitude < minDist){
                particle.GetComponent<ParticleSystem>().Stop();
                particles.Remove(particle);
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

        GameObject particle = Instantiate(brightstoneParticles, locationToSpawn, transform.rotation);
        particles.Add(particle.transform);
        var main = particle.GetComponent<ParticleSystem>().main;

        main.startSize = brightstonesData[(int)bType].sizeOnStart;
        main.startColor = brightstonesData[(int)bType].color;
    }
}