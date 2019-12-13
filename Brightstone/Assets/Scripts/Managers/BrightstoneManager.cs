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
    [SerializeField] private BrightstoneData[] brightstonesData;

    private List<Transform> particles = new List<Transform>();
    private GameManager gM;

    private void Start(){
        gM = GameManager.Instance;
    }

    private void Update(){
        if (gM.activeRoom.GetRoomsBehaviour().Complete)
            MakeParticlesMovement();
    }

    private void MakeParticlesMovement(){
        foreach (Transform particle in particles){
            
        }
    }

    public void OnEnemyDeath(EnemyType enemyType, Vector3 locationToSpawn){
        particles.Add(Instantiate(brightstoneParticles, locationToSpawn, transform.rotation).transform);
    }
}