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
    Melee11 = 0,
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
    Count,
    None
}
public struct PerAttackTurn{
	public bool isAssigned;
	public int enemyType;
	public int enemyIndex;
	public int attacksLeft;

    public PerAttackTurn(bool ignore = true){
        isAssigned = false;
        enemyType = -1;
        enemyIndex = -1;
        attacksLeft = -1;
    }
    public PerAttackTurn(bool Assigned, int EnemyType, int EnemyIndex, int AttackCounter){
        isAssigned = Assigned;
        enemyType = EnemyType;
        enemyIndex = EnemyIndex;
        attacksLeft = AttackCounter;
    }
}
public struct PerHPTurn{
	public bool isAssigned;
	public int enemyType;
	public int enemyIndex;
	public float hPThreshold;
    public PerHPTurn(bool ignore = true){
        isAssigned = false;
        enemyType = -1;
        enemyIndex = -1;
        hPThreshold = -1;
    }
     public PerHPTurn(bool Assigned, int EnemyType, int EnemyIndex, float HPThreshold){
        isAssigned = Assigned;
        enemyType = EnemyType;
        enemyIndex = EnemyIndex;
        hPThreshold = HPThreshold;
    }

}
public enum TurnType{
    Attack = 0,
    HP,
    Count,
    None
}
public class EnemyBehaviour : MonoBehaviour{
    #region "Placeholders to be deleted"
    //Code without references should be deleted
    //Code with references to this code should be reworked to not use it
    public Strategies warriorStrategy{
        get{ 
            Debug.LogError("This variable is old code that should not be used");
            return Strategies.None;
        }
    }
    public void WarriorAddedToChase(GameObject go){
        Debug.LogError("This function is old code that should not be used");
    }
    public void AddEnemyToBehaviour(GameObject go){
        Debug.LogError("This function is old code that should not be used");
    }
    public void ChangeStrategy(){
        Debug.LogError("This function is old code that should not be used");
    }
    public void Death(GameObject go){
        Debug.LogError("This function is old code that should not be used");
    }
    #endregion
    private static EnemyBehaviour instance;

    public static EnemyBehaviour Instance{
        get {
            instance = FindObjectOfType<EnemyBehaviour>();
            if(instance == null) {
                GameObject go = new GameObject("Managers");
                instance = go.AddComponent<EnemyBehaviour>();
            }
            return instance;
        }
    }

    public Strategies currentStrategy{ get; private set;}
    private List<List<EnemyBase>> enemiesInRoom;
    private int MaxEnemiesPerTurn = 3;
    List<PerAttackTurn> enemiesInPerAttackTurn;
    PerHPTurn[] enemiesInPerHPTurn;
    TurnType turn = TurnType.None;

    delegate void OnStrategyExit();
    private OnStrategyExit onStrategyExit;

    void Awake(){
        currentStrategy = Strategies.None;
        onStrategyExit = onNoStrategyExit;
        ClearLists();
    }
    void Start(){
        FillEnemyList();
    }

    void Update(){
        TurnCounter();
    }

    private void TurnCounter(){
        bool turnShouldPass = true;
        switch(turn){
            case TurnType.Attack:
                turnShouldPass = attackStrategy();
            break;
            case TurnType.HP:
            break;
            default:
            break;
        }
        if(turnShouldPass){
            onStrategyExit.Invoke();
            InitializeStrategy();
        }
    }

    #region "Strategy initializing"
    private void ClearLists(){
        enemiesInRoom = new List<List<EnemyBase>>();
        for(int i = 0; i < (int)EnemyType.Count; i++){
            List<EnemyBase> l = new List<EnemyBase>();
            enemiesInRoom.Add(l);
        }

        enemiesInPerHPTurn = new PerHPTurn[MaxEnemiesPerTurn];
        for(int i = 0; i < MaxEnemiesPerTurn; i++){
            enemiesInPerHPTurn[i] = new PerHPTurn(true);
        }

        enemiesInPerAttackTurn = new List<PerAttackTurn>();
    }

    private void FillEnemyList(){
        List<EnemyBase> enemyList = GameManager.Instance.activeRoom.GetRoomsBehaviour().GetEnemies();
        foreach(EnemyBase e in enemyList){
            enemiesInRoom[(int)e.GetEnemyType()].Add(e); 
        }
    }

    private void InitializeStrategy(){
        EnemyType pickedEnemyType = PickEnemyType();
        switch(pickedEnemyType){
            case EnemyType.Archer:
                turn = TurnType.Attack;
            break;
            case EnemyType.Boss:
                turn = TurnType.None;
            break;
            case EnemyType.Lancer:
                turn = TurnType.HP;
            break;
            case EnemyType.Samurai:
                turn = TurnType.HP;
            break;
            case EnemyType.Warrior:
                turn = TurnType.HP;
            break;
            default:
                turn = TurnType.None;
            break;
        }

        FillTurnLists();
        if(turn == TurnType.HP){
            PickStrategy();
        }
    }

    private EnemyType PickEnemyType(){
        //Picks a random EnemyType for a turn
        List<EnemyType> enemyTypesInRoom = new List<EnemyType>();
        foreach(List<EnemyBase> list in enemiesInRoom){
            if (list.Count > 0) {enemyTypesInRoom.Add(list[0].GetEnemyType());}
        }
        EnemyType pick = EnemyType.Count; //value used as error check
        if(enemyTypesInRoom.Count > 0){
            int random = Random.Range(0,enemyTypesInRoom.Count);
            pick = enemyTypesInRoom[random];
        }
        return pick;
    }

    private void FillTurnLists(){
        enemiesInPerAttackTurn.Clear();

        if(turn == TurnType.HP){
            float hPPromedy = HPPromedy();
            List<EnemyBase> enemiesToGetInTurn = GetRandomEnemies(enemiesInPerHPTurn.Length, EnemyType.Warrior); //hardcoded enemy type
            for(int i = 0; i < enemiesInPerHPTurn.Length; i++){

                PerHPTurn t;
                if(enemiesToGetInTurn.Count > i){
                    t.enemyType = (int)enemiesToGetInTurn[i].GetEnemyType();
                    t.enemyIndex = enemiesInRoom[(int)enemiesToGetInTurn[i].GetEnemyType()].IndexOf(enemiesToGetInTurn[i]);
                    float random = Random.Range(-1.0f, 1.0f);
                    t.hPThreshold = hPPromedy + (enemiesToGetInTurn[i].GetMaxHP()/ 5 * random);
                    if(enemiesToGetInTurn[i].GetHP() < t.hPThreshold){
                        t.hPThreshold = 0;
                    }
                    t.isAssigned = true;
                } else {
                    t = new PerHPTurn(true);
                }

                enemiesInPerHPTurn[i] = t;

            }
        } 
        else if (turn == TurnType.Attack){
            int randomAttackAmmount = Random.Range(1, 4);
            foreach(EnemyBase e in enemiesInRoom[(int)EnemyType.Archer]){
                if(e.GetHP() > 0){
                    PerAttackTurn t;
                    t.attacksLeft = randomAttackAmmount;
                    t.enemyType = (int)e.GetEnemyType();
                    t.enemyIndex = enemiesInRoom[(int)e.GetEnemyType()].IndexOf(e);
                    t.isAssigned = true;
                    enemiesInPerAttackTurn.Add(t);
                }
            }
            for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
                PerHPTurn hp = new PerHPTurn(true);
                enemiesInPerHPTurn[i] = hp;
            }
        }
    }

    private float HPPromedy(){
        float totalHP = 0.0f;
        int totalEnemies = 0;
        List<EnemyBase> enemyList = GameManager.Instance.activeRoom.GetRoomsBehaviour().GetEnemies();
        foreach(EnemyBase e in enemyList){
            if(e.GetHP() > 0){
                totalHP += e.GetHP();
                totalEnemies++;
            }
        }
        return totalHP/totalEnemies;
    }

    private List<EnemyBase> GetRandomEnemies(int ammount, EnemyType type){
        List<EnemyBase> enemyList = new List<EnemyBase>();

        List<EnemyBase> enemyListClone = new List<EnemyBase>();

        foreach(EnemyBase e in enemiesInRoom[(int)type]){
            if(e.GetHP() > 0){
                enemyListClone.Add(e);
            }
        }

        for(int i = 0; i < ammount; i++){
            int random = Random.Range(0,enemyListClone.Count);
            enemyList[i] = enemyListClone[random];
            enemyListClone.Remove(enemyListClone[random]);
        }
        return enemyList;
    }

    private void PickStrategy(){
        int enemiesInTurn = 0;
        foreach(PerHPTurn t in enemiesInPerHPTurn){
            if(t.isAssigned){
                enemiesInTurn++;
            }
        }
        if(enemiesInTurn > MaxEnemiesPerTurn){ enemiesInTurn = MaxEnemiesPerTurn;}
        int randomStrategy = Random.Range(0,5);
        currentStrategy = (Strategies)randomStrategy + enemiesInTurn;
    }
    #endregion
    #region "Strategy management"

    private void onNoStrategyExit(){}
    [Header("Attack turn timer")]
    [SerializeField]float timeBetweenAttacks;
    float attackTimer = 0.0f;
    int subTurn = 0;
    private bool attackStrategy(){
        bool turnShouldPass = true;
        foreach(PerAttackTurn t in enemiesInPerAttackTurn){
            if(t.attacksLeft > 0){ turnShouldPass = false;}
        }
        if(!turnShouldPass){
            attackTimer += Time.deltaTime;
            if(attackTimer >= timeBetweenAttacks){
                attackTimer = 0.0f;
                if(subTurn >= enemiesInPerAttackTurn.Count){ subTurn = 0; }

                if(enemiesInPerAttackTurn[subTurn].attacksLeft > 0){
                    PerAttackTurn t = enemiesInPerAttackTurn[subTurn];
                    enemiesInRoom[t.enemyType][t.enemyIndex].isMyAttackingTurn = true;
                    t.attacksLeft--;
                    enemiesInPerAttackTurn[subTurn] = t;
                    subTurn++;
                }
            }
        } else {
            subTurn = 0;
            attackTimer = 0.0f;
        }

        return turnShouldPass;
    }
    
    private bool HPStrategy(){
        bool turnShouldPass = true;
        foreach(PerHPTurn t in enemiesInPerHPTurn){
            if(t.hPThreshold < enemiesInRoom[t.enemyType][t.enemyIndex].GetHP()){
                turnShouldPass = false;
            }
        }
        if(!turnShouldPass){
            
        }
        return turnShouldPass;
    }
    #endregion
}
#endregion