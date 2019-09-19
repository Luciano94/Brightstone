using System.Collections.Generic;
using UnityEngine;
#region "Old Code"
/*
public enum WarriorStrategy{
        Melee11,
        Melee12,
        Melee13,
        Melee14,
        Melee15,
        Melee21,
        Melee22,
        Melee23,
        Melee24,
        Melee25,
        Melee31,
        Melee32,
        Melee33,
        Melee34,
        Melee35,
        Count
    }

public class EnemyBahaviour : MonoBehaviour{
    private static EnemyBahaviour instance;

    public static EnemyBahaviour Instance{
        get {
            instance = FindObjectOfType<EnemyBahaviour>();
            if(instance == null) {
                GameObject go = new GameObject("Managers");
                instance = go.AddComponent<EnemyBahaviour>();
            }
            return instance;
        }
    }

    [Header("Warrior Variables")]
    [SerializeField] private int maxWarriorsAtking;
    [SerializeField] private float timePerWarriorAttack = 0.5f;
    [SerializeField] private float timePerWarriorFastAttack = 0.25f;
    
    public WarriorStrategy warriorStrategy;

    private int strategyIndex;
    private List<List<EnemyBase>> enemies;
    private List<EnemyWarrior> warriorsChasers;
    private float warriorTimeLeft;
    private int enemiesLeft = 0;
    private bool enemyAdded = false;

    private void Awake(){
        enemies = new List<List<EnemyBase>>();
        for (int i = 0; i < (int)EnemyType.Count; i++)
            enemies.Add(new List<EnemyBase>());

        warriorsChasers = new List<EnemyWarrior>();
    }

    private void Update(){
        if (warriorsChasers.Count > 0){
            if (((int)warriorStrategy + 1) % 5 != 0){
                warriorTimeLeft -= Time.deltaTime;

                if (warriorTimeLeft <= 0.0f){
                    if (warriorStrategy == WarriorStrategy.Melee32)
                        warriorTimeLeft = timePerWarriorFastAttack;
                    else
                        warriorTimeLeft = timePerWarriorAttack;
                    
                    //EnemyMelee warrior = warriorsChasers[0]; // Estas 4 lineas laguean todo :c
                    //warrior.isMyAttackingTurn = true;
                    //warriorsChasers.Remove(warrior);
                    //warriorsChasers.Add(warrior);

                    int index = 0;
                    foreach (EnemyWarrior w in warriorsChasers){
                        w.chaserIndex = index;
                        index++;
                    }
                }
            }
            else{
                foreach (EnemyBase warrior in warriorsChasers)
                    warrior.isMyAttackingTurn = true;
            }
        }
    }

    private void LateUpdate(){
        if(enemyAdded){
            enemyAdded = false;
            UpdateStrategy();
        }
    }

#region StrategyManagement
    public void AddEnemyToBehaviour(GameObject enemy){
        enemiesLeft++;
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        enemies[(int)enemyBase.GetEnemyType()].Add(enemyBase);

        enemyAdded = true;
    }

    public void WarriorOnLowHealth(EnemyWarrior thisEnemy){
        if (enemies[(int)EnemyType.Warrior].Count <= 3) return;

        thisEnemy.isMyAttackingTurn = false;
        warriorsChasers.Remove(thisEnemy);
    }

    public void Death(GameObject thisEnemy){
        EnemyBase enemyBase = thisEnemy.GetComponent<EnemyBase>();
        enemies[(int)enemyBase.GetEnemyType()].Remove(enemyBase);

        EnemyWarrior melee = thisEnemy.GetComponent<EnemyWarrior>();
        if (melee) warriorsChasers.Remove(melee);

        enemiesLeft--;

        UpdateStrategy();
    }
    
    public void WarriorAddedToChase(GameObject thisEnemy){
        warriorsChasers.Add(thisEnemy.GetComponent<EnemyWarrior>());

        int index = 0;
        int countChasing = 0;
        int indexOfFarest = -1;
        Vector3 farestDistance = Vector3.zero;

        Vector3 playerPos = GameManager.Instance.PlayerPos;

        foreach (EnemyBase enemy in enemies[(int)EnemyType.Warrior]){
            if (!enemy.IsInGuardState()){
                countChasing++;
                Vector3 dist = playerPos - enemy.transform.position;
                if (dist.magnitude > farestDistance.magnitude){
                    farestDistance = dist;
                    indexOfFarest = index;
                }
            }

            index++;
        }

        if (countChasing >= maxWarriorsAtking){
            EnemyBase enemy = enemies[(int)EnemyType.Warrior][indexOfFarest];
            enemy.isMyAttackingTurn = false;
            enemy.ForceToGuardState();
            warriorsChasers.Remove(enemy.GetComponent<EnemyWarrior>());
        }
    }

    public void ChangeStrategy(){
        //strategyIndex = Random.Range(0, 5);
        strategyIndex = 4;
        warriorTimeLeft = 1.0f;
    }

    public void UpdateStrategy(){
        if(enemiesLeft > 0){
            UpdateWarriorsStrategy();
            UpdateBossStrategy();
        }
    }

    public void UpdateWarriorsStrategy(){
        if (enemies[(int)EnemyType.Warrior].Count == 0) return;

        int countChasing = 0;
            
        foreach (EnemyBase enemy in enemies[(int)EnemyType.Warrior])
            if (!enemy.IsInGuardState())
                countChasing++;

        if (countChasing < maxWarriorsAtking || countChasing == enemies[(int)EnemyType.Warrior].Count)
            foreach (EnemyBase enemy in enemies[(int)EnemyType.Warrior])
                if (enemy.IsInGuardState() && countChasing < maxWarriorsAtking){
                    countChasing++;
                    enemy.Chase();
                    warriorsChasers.Add(enemy.GetComponent<EnemyWarrior>());
                }

        if (countChasing > 0)
            warriorStrategy = (WarriorStrategy)(strategyIndex + 10);
            //warriorStrategy = (WarriorStrategy)(strategyIndex * (countChasing - 1)); // Esto lo va a hacer cuando tenga todas las estrategias establecidas
    }

    public void UpdateBossStrategy(){
        if (enemies[(int)EnemyType.Boss].Count > 0)
                enemies[(int)EnemyType.Boss][0].Chase();
    }
#endregion
}*/
#endregion
#region "New code"
public enum Strategies{
    None,
    Melee11,
    Melee12,
    Melee13,
    Melee14,
    Melee15,
    Melee21,
    Melee22,
    Melee23,
    Melee24,
    Melee25,
    Melee31,
    Melee32,
    Melee33,
    Melee34,
    Melee35,
    Count
}

public class EnemyBahaviour : MonoBehaviour{
    private static EnemyBahaviour instance;

    public static EnemyBahaviour Instance{
        get {
            instance = FindObjectOfType<EnemyBahaviour>();
            if(instance == null) {
                GameObject go = new GameObject("Managers");
                instance = go.AddComponent<EnemyBahaviour>();
            }
            return instance;
        }
    }

    #region "Placeholders to be deleted"
    //Code without references should be deleted
    //Code with references to this code should be reworked to not use it
    public Strategies warriorStrategy{
        get{ 
            Debug.LogError("This is old code that should not be used");
            return Strategies.None;
        }
    }
    public void WarriorAddedToChase(GameObject go){
        Debug.LogError("This is old code that should not be used");
    }
    public void AddEnemyToBehaviour(GameObject go){
        Debug.LogError("This is old code that should not be used");
    }
    public void ChangeStrategy(){
        Debug.LogError("This is old code that should not be used");
    }
    public void Death(GameObject go){
        Debug.LogError("This is old code that should not be used");
    }
    #endregion


}
#endregion