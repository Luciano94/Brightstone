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

    public Strategies currentStrategy;//{ get; private set;}
    private List<List<EnemyBase>> enemiesInRoom;
    private int MaxEnemiesPerTurn = 3;
    List<PerAttackTurn> enemiesInPerAttackTurn;
    PerHPTurn[] enemiesInPerHPTurn;
    TurnType turn = TurnType.None;
    private int enemiesLeft = 0;

    delegate void OnStrategyExit();
    private OnStrategyExit onStrategyExit;

    void Awake(){
        currentStrategy = Strategies.None;
        onStrategyExit = onNoStrategyExit;
        ClearLists();
    }

    void Update(){
        if (enemiesLeft > 0)
            TurnCounter();
    }

    private void TurnCounter(){
        bool turnShouldPass = true;
        switch(turn){
            case TurnType.Attack:
                turnShouldPass = AttackStrategy();
            break;
            case TurnType.HP:
                turnShouldPass = HPStrategy();
            break;
            default:
                turnShouldPass = true;
            break;
        }
        if(turnShouldPass){
            onStrategyExit();
            InitializeStrategy();
        }
    }

    public void onEnemyDeath(EnemyBase enemy){
        enemiesLeft--;
        EnemyType type = enemy.GetEnemyType();
        if(type == EnemyType.Archer){
            foreach(PerAttackTurn t in enemiesInPerAttackTurn){
                if(enemy == enemiesInRoom[(int)EnemyType.Archer][t.enemyIndex]){
                    enemiesInPerAttackTurn.Remove(t);
                    break;
                }
            }
        } else {
            int index = enemiesInRoom[(int)type].IndexOf(enemy);
            for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
                if(enemiesInPerHPTurn[i].enemyType == (int)type && 
                enemiesInPerHPTurn[i].enemyIndex == index){
                    PerHPTurn t = new PerHPTurn(true);
                    enemiesInPerHPTurn[i] = t;   
                }
            }
        }
        enemiesInRoom[(int)type].Remove(enemy);
    }

    #region "Strategy initializing"
    private void ClearLists(){
        enemiesInRoom = new List<List<EnemyBase>>();
        for(int i = 0; i < (int)EnemyType.Count; i++)
            enemiesInRoom.Add(new List<EnemyBase>());

        enemiesInPerHPTurn = new PerHPTurn[MaxEnemiesPerTurn];
        for(int i = 0; i < MaxEnemiesPerTurn; i++){
            enemiesInPerHPTurn[i] = new PerHPTurn(true);
        }

        enemiesInPerAttackTurn = new List<PerAttackTurn>();
    }

    public void FillEnemyList(){
        List<EnemyBase> enemyList = GameManager.Instance.activeRoom.GetRoomsBehaviour().GetEnemies();
        foreach(EnemyBase e in enemyList){
            enemiesInRoom[(int)e.GetEnemyType()].Add(e);
            enemiesLeft++;
        }
    }

    private void InitializeStrategy(){
        //EnemyType pickedEnemyType = PickEnemyType();
        EnemyType pickedEnemyType = EnemyType.Warrior;
        switch(pickedEnemyType){
            case EnemyType.Archer:
                turn = TurnType.Attack;
            break;
            case EnemyType.Boss:
                turn = TurnType.HP;
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
            //List<EnemyBase> enemiesToGetInTurn = GetRandomEnemies(enemiesInPerHPTurn.Length, EnemyType.Warrior); //hardcoded enemy type
            List<EnemyBase> enemiesToGetInTurn = GetEnemies(enemiesInPerHPTurn.Length, EnemyType.Warrior);
            for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
                if(enemiesInPerHPTurn[i].isAssigned){
                    enemiesInRoom[enemiesInPerHPTurn[i].enemyType][enemiesInPerHPTurn[i].enemyIndex].enemyIndex = -1;
                }
                PerHPTurn t;
                if(enemiesToGetInTurn.Count > i){
                    enemiesToGetInTurn[i].enemyIndex = i;
                    t.enemyType = (int)enemiesToGetInTurn[i].GetEnemyType();
                    t.enemyIndex = enemiesInRoom[(int)enemiesToGetInTurn[i].GetEnemyType()].IndexOf(enemiesToGetInTurn[i]);
                    float random = Random.Range(-1.0f, 1.0f);
                    t.hPThreshold = hPPromedy + (enemiesToGetInTurn[i].GetMaxHP()/ 5 * random);
                    if(enemiesToGetInTurn[i].GetHP() < t.hPThreshold){
                        t.hPThreshold = 0;
                    }
                    enemiesToGetInTurn[i].Chase();
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

    private List<EnemyBase> GetEnemies(int ammount, EnemyType type){
        List<EnemyBase> enemyList = new List<EnemyBase>();
        int count = enemiesInRoom[(int)type].Count;
        if (count > MaxEnemiesPerTurn)
            count = ammount;

        for(int i = 0; i < count; i++)
            enemyList.Add(enemiesInRoom[(int)type][i]);

        return enemyList;
    }

    private void PickStrategy(){
        int enemiesInTurn = 0;
        foreach(PerHPTurn t in enemiesInPerHPTurn){
            if(t.isAssigned && t.enemyType == (int)EnemyType.Warrior){
                enemiesInTurn++;
            }
        }

        if (enemiesInTurn == 0) return;

        if(enemiesInTurn >= MaxEnemiesPerTurn){ enemiesInTurn = MaxEnemiesPerTurn;}
        enemiesInTurn--;
        int randomStrategy = Random.Range(0,5);
        currentStrategy = (Strategies)(randomStrategy + enemiesInTurn * 5);
    }
    #endregion

    #region "Strategy management"

    private void onNoStrategyExit(){}
    [Header("Attack turn timer")]
    [SerializeField]float timeBetweenRangedAttacks;
    float attackTimer = 0.0f;
    int subTurn = 0;
    private bool AttackStrategy(){
        bool turnShouldPass = true;
        foreach(PerAttackTurn t in enemiesInPerAttackTurn){
            if(t.attacksLeft > 0){ turnShouldPass = false;}
        }
        if(!turnShouldPass){
            attackTimer += Time.deltaTime;
            if(attackTimer >= timeBetweenRangedAttacks){
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
            if(t.isAssigned/*&& t.hPThreshold < enemiesInRoom[t.enemyType][t.enemyIndex].GetHP()*/){
                turnShouldPass = false;
            }
        }
        if(!turnShouldPass){
            switch(currentStrategy){
                case Strategies.Melee11:
                    Melee35();
                break;
                case Strategies.Melee12:
                    Melee35();
                break;
                case Strategies.Melee13:
                    Melee35();
                break;
                case Strategies.Melee14:
                    Melee35();
                break;
                case Strategies.Melee15:
                    Melee35();
                break;
                case Strategies.Melee21:
                    Melee31();
                break;
                case Strategies.Melee22:
                    Melee32();
                break;
                case Strategies.Melee23:
                    Melee33();
                break;
                case Strategies.Melee24:
                    Melee34();
                break;
                case Strategies.Melee25:
                    Melee35();
                break;
                case Strategies.Melee31:
                    Melee31();
                break;
                case Strategies.Melee32:
                    Melee32();
                break;
                case Strategies.Melee33:
                    Melee33();
                break;
                case Strategies.Melee34:
                    Melee34();
                break;
                case Strategies.Melee35:
                    Melee35();
                break;
                default:
                    turnShouldPass = true;
                    subTurn = 0;
                    attackTimer = 0.0f;
                break;
            } 
        } else {
            subTurn = 0;
            attackTimer = 0.0f;
        }
        return turnShouldPass;
    }

    #region "Strategy 31"
    private bool str31TwoFirstAttack = true;
    private int str31RoundCounter = 0;
    private float str31Timer = 0.0f;
    private void Melee31(){
        //onStrategyExit = OnMelee31Exit;
        str31Timer += Time.deltaTime;
        if(str31RoundCounter >= 3){
            if(str31Timer >= 1.0f){
                str31Timer = 0.0f;
                str31RoundCounter = 0;
            }
        } else if(str31Timer >= 0.3f){
            str31Timer = 0.0f;
            if(str31TwoFirstAttack){
                if(enemiesInPerHPTurn[0].isAssigned){
                    enemiesInRoom[enemiesInPerHPTurn[0].enemyType][enemiesInPerHPTurn[0].enemyIndex].isMyAttackingTurn=true;
                }
                if(enemiesInPerHPTurn[1].isAssigned){
                    enemiesInRoom[enemiesInPerHPTurn[1].enemyType][enemiesInPerHPTurn[1].enemyIndex].isMyAttackingTurn=true;
                }
            } else {
                if(enemiesInPerHPTurn[2].isAssigned){
                    enemiesInRoom[enemiesInPerHPTurn[2].enemyType][enemiesInPerHPTurn[2].enemyIndex].isMyAttackingTurn=true;
            }
            str31RoundCounter++;
        }
        str31TwoFirstAttack = !str31TwoFirstAttack;
        }
    }
    private void OnMelee31Exit(){
        str31TwoFirstAttack = true;
        str31RoundCounter = 0;
        str31Timer = 0.0f;
        onStrategyExit = onNoStrategyExit;
    }
    #endregion
    #region "Strategy 32"
    private float str32Timer = 0.0f;
    private int str32RoundCounter = 0;
    private void Melee32(){
        //onStrategyExit = OnMelee32Exit;
        str32Timer += Time.deltaTime;
        if(str32RoundCounter > 2){
            if(str32Timer >= 0.5f){
                str32RoundCounter = 0;
                str32Timer = 0.0f;
            }
        } else {
            if(str32Timer >= 0.3f){
                PerHPTurn t = enemiesInPerHPTurn[str32RoundCounter];
                if(t.isAssigned){
                    enemiesInRoom[t.enemyType][t.enemyIndex].isMyAttackingTurn = true;
                }
                str32RoundCounter++;
                str32Timer = 0.0f;
            }
        }
    }
    private void OnMelee32Exit(){
        str32Timer = 0.0f;
        str32RoundCounter = 0;
        onStrategyExit = onNoStrategyExit;
    }
    #endregion
    #region "Strategy 33"
    private int str33AttackCounter = 0;
    private float str33Timer = 0.0f;

    private void Melee33(){
        //onStrategyExit = OnMelee33Exit;
        str33Timer += Time.deltaTime;
        if(str33AttackCounter >= 9){
            if(str33Timer >= 0.5f){
                str33AttackCounter = 0;
                str33Timer = 0.0f;
            }
        } else {
            if(str33Timer >= 0.5f){
                str33Timer = 0.0f;
                int attacker = Random.Range(0,3);
                PerHPTurn t = enemiesInPerHPTurn[attacker];
                if(t.isAssigned){
                    enemiesInRoom[t.enemyType][t.enemyIndex].isMyAttackingTurn = true;
                }
                str33AttackCounter++;
            }
        }
    }
    private void OnMelee33Exit(){
        str33Timer = 0.0f;
        str33AttackCounter = 0;
        onStrategyExit = onNoStrategyExit;
    }
    #endregion
    #region "Strategy 34"
    private bool str34ShouldFeint = false;
    private bool str34Waiting = false;
    private float str34Timer = 0.0f;
    private int str34Counter = 0;
    private float str34AttackTimer = 0.0f;
    private void Melee34(){
        //onStrategyExit = OnMelee34Exit;
        str34Timer += Time.deltaTime;
        if(str34Waiting){
            if(str34Timer >= 0.5f){
                str34Waiting = false;
                str34Timer = 0.0f;
            }
        } else {
            if(str34Timer >= 0.5f){
                str34Timer = 0.0f;
                str34ShouldFeint = false;
                int random = Random.Range(0,2);
                if(random > 0){str34ShouldFeint = true;}
            
                PerHPTurn t = enemiesInPerHPTurn[str34Counter];

                if(t.isAssigned){
                    if(str34ShouldFeint){ str34AttackTimer = 0.2f;}
                    enemiesInRoom[t.enemyType][t.enemyIndex].isMyAttackingTurn = true;
                    enemiesInRoom[t.enemyType][t.enemyIndex].feinting = str34ShouldFeint;
                }

                str34Counter++;
                if(str34Counter > 2){ str34Counter = 0;}
            }
        }
        if(str34AttackTimer > 0){
            str34AttackTimer -= Time.deltaTime;
            PlayerCombat p = GameManager.Instance.playerCombat;
            if(p.isAttacking || p.isParrying){
                for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
                    PerHPTurn t = enemiesInPerHPTurn[i];
                    if(t.isAssigned && str34Counter -1 != i){
                        enemiesInRoom[t.enemyType][t.enemyIndex].isMyAttackingTurn = true;
                    }
                }
            }
        }
    }
    private void OnMelee34Exit(){
        str34ShouldFeint = false;
        str34Waiting = false;
        str34Timer = 0.0f;
        str34Counter = 0;
        str34AttackTimer = 0.0f;
        onStrategyExit = onNoStrategyExit;
    }
    #endregion
    private void Melee35(){
        for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
            PerHPTurn t = enemiesInPerHPTurn[i];
            if(t.isAssigned){
                enemiesInRoom[t.enemyType][t.enemyIndex].isMyAttackingTurn = true;
            }
        }
    }
    #endregion
}
#endregion