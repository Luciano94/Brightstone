using UnityEngine;
public enum EnemyType{
    Warrior=0,
    Samurai,
    Archer,
    Lancer,
    Boss,
    Count,
}

public class EnemyManager : MonoBehaviour{
    public float[] enePercents;
    private float plusPercent = 10.0f;
    private static EnemyManager instance;

    public static EnemyManager Instance {
        get {
            instance = FindObjectOfType<EnemyManager>();
            if(instance == null) {
                GameObject go = new GameObject("EnemyManager");
                instance = go.AddComponent<EnemyManager>();
            }
            return instance;
        }
    }

    private void Start() {
        float equality = 100 / (float)EnemyType.Count;
        enePercents = new float[(int)EnemyType.Count];
        for (int i = 0; i < enePercents.Length; i++)
            enePercents[i] = equality;
    }

    public void PlusPercent(EnemyType eType){
        for (int i = 0; i < enePercents.Length; i++){
            if((EnemyType)i == eType){
                if(enePercents[i] + plusPercent < 100)
                    enePercents[i] += plusPercent;
                else
                    enePercents[i] = 100;
            }
            else{
                if(enePercents[i]-plusPercent > 0)
                    enePercents[i] -= plusPercent;
                else
                    enePercents[i] = 0;
            }
        }
    }    
}
