using System.Collections.Generic;
using UnityEngine;
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
    Boss,
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
        InitLists();
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
            case TurnType.Boss:
                turnShouldPass = BossStrategy();
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
            int deadEnemyIndex = -1;
            for(int i = 0; i < enemiesInPerAttackTurn.Count; i++){
                PerAttackTurn t = enemiesInPerAttackTurn[i];
                if(enemy == enemiesInRoom[(int)type][t.enemyIndex]){
                    deadEnemyIndex = t.enemyIndex;
                    PerAttackTurn newTurn = new PerAttackTurn(false,t.enemyType,t.enemyIndex,t.attacksLeft);
                    enemiesInPerAttackTurn[enemiesInPerAttackTurn.IndexOf(t)] = newTurn;
                    i = enemiesInPerAttackTurn.Count;
                }
            }
            if(deadEnemyIndex >= 0){
                for(int i = 0; i < enemiesInPerAttackTurn.Count; i++){
                    PerAttackTurn t = enemiesInPerAttackTurn[i];
                    if(t.isAssigned && t.enemyIndex > deadEnemyIndex){
                        PerAttackTurn newTurn = new PerAttackTurn(t.isAssigned,t.enemyType,t.enemyIndex -1, t.attacksLeft);
                        enemiesInPerAttackTurn[enemiesInPerAttackTurn.IndexOf(t)] = newTurn;
                    }
                }
            }
        } else {
            int index = enemiesInRoom[(int)type].IndexOf(enemy);
            for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
                if(enemiesInPerHPTurn[i].enemyType == (int)type && 
                enemiesInPerHPTurn[i].enemyIndex == index){
                    PerHPTurn t = new PerHPTurn(true);
                    enemiesInPerHPTurn[i] = t;   
                } else if( enemiesInPerHPTurn[i].enemyType == (int)type && enemiesInPerHPTurn[i].enemyIndex > index){
                    PerHPTurn t = enemiesInPerHPTurn[i];
                    PerHPTurn t2 = new PerHPTurn(t.isAssigned ,t.enemyType, t.enemyIndex -1, t.hPThreshold);
                    enemiesInPerHPTurn[i] = t2; 
                }
            }
        }
        enemiesInRoom[(int)type].Remove(enemy);
    }

    #region "Strategy initializing"
    private void InitLists(){
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
        EnemyType pickedEnemyType = PickEnemyType();
        //EnemyType pickedEnemyType = EnemyType.Warrior;
        switch(pickedEnemyType){
            case EnemyType.Archer:
                turn = TurnType.Attack;
            break;
            case EnemyType.Boss:
                turn = TurnType.Boss;
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
        //clear lists
        enemiesInPerAttackTurn.Clear();
        for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
            if(enemiesInPerHPTurn[i].isAssigned){
                enemiesInRoom[enemiesInPerHPTurn[i].enemyType][enemiesInPerHPTurn[i].enemyIndex].enemyIndex = -1;
            }
            PerHPTurn hp = new PerHPTurn(true);
            enemiesInPerHPTurn[i] = hp;
        }

        //Fill lists
        if(turn == TurnType.HP){
            float hPPromedy = HPPromedy();
            List<EnemyBase> enemiesToGetInTurn = GetRandomEnemies(enemiesInPerHPTurn.Length, EnemyType.Warrior); //hardcoded enemy type
            for(int i = 0; i < enemiesInPerHPTurn.Length; i++){
                
                PerHPTurn t;
                if(enemiesToGetInTurn.Count > i){
                    enemiesToGetInTurn[i].enemyIndex = i;
                    t.enemyType = (int)enemiesToGetInTurn[i].GetEnemyType();
                    t.enemyIndex = enemiesInRoom[(int)enemiesToGetInTurn[i].GetEnemyType()].IndexOf(enemiesToGetInTurn[i]);
                    
                    float random = Random.Range(-1.0f, 0.0f);
                    t.hPThreshold = hPPromedy + (enemiesToGetInTurn[i].GetMaxHP()/ 5 * random);
                    
                    //if(enemiesToGetInTurn[i].GetHP() < t.hPThreshold){ t.hPThreshold = 0; }
                    if(enemiesToGetInTurn[i].GetHP() <= 50){ t.hPThreshold = 0; }

                    enemiesToGetInTurn[i].Chase();
                    t.isAssigned = true;
                } else {
                    t = new PerHPTurn(true);
                }

                enemiesInPerHPTurn[i] = t;

            }
        } else if (turn == TurnType.Attack){
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
        } else if (turn == TurnType.Boss){
            int index = 0;
            foreach(EnemyBase e in enemiesInRoom[(int)EnemyType.Boss]){
                e.enemyIndex = index;
                e.Chase();
                index++;
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
            if(enemyListClone.Count > 0){
                int random = Random.Range(0,enemyListClone.Count);
                enemyList.Add(enemyListClone[random]);
                enemyListClone.Remove(enemyListClone[random]);
            }
        }
        return enemyList;
    }

    private List<EnemyBase> GetEnemies(int ammount, EnemyType type){
        List<EnemyBase> enemyList = new List<EnemyBase>();
        int count = enemiesInRoom[(int)type].Count;
        if (count > MaxEnemiesPerTurn)
            count = MaxEnemiesPerTurn;

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

    private bool BossStrategy(){
        bool turnShouldPass = true;
        if(enemiesInRoom[(int)EnemyType.Boss].Count > 0){
            turnShouldPass = false;
            foreach(EnemyBase e in enemiesInRoom[(int)EnemyType.Boss]){
                e.isMyAttackingTurn = true;
            }
        }

        return turnShouldPass;
    }
    [Header("Attack turn timer")]
    private float timeBetweenRangedAttacks = 0.5f;
    float attackTimer = 0.0f;
    int subTurn = 0;
    private bool AttackStrategy(){
        bool turnShouldPass = true;
        foreach(PerAttackTurn t in enemiesInPerAttackTurn){
            if(t.isAssigned && t.attacksLeft > 0){ turnShouldPass = false;}
        }
        if(!turnShouldPass){
            attackTimer += Time.deltaTime;
            if(attackTimer >= timeBetweenRangedAttacks){
                if(subTurn >= enemiesInPerAttackTurn.Count){ subTurn = 0; }
                if(enemiesInPerAttackTurn[subTurn].isAssigned && enemiesInPerAttackTurn[subTurn].attacksLeft > 0){
                    attackTimer = 0.0f;
                    PerAttackTurn t = enemiesInPerAttackTurn[subTurn];
                    enemiesInRoom[t.enemyType][t.enemyIndex].isMyAttackingTurn = true;
                    t.attacksLeft--;
                    enemiesInPerAttackTurn[subTurn] = t;
                }
                subTurn++;
            }
        } else {
            subTurn = 0;
            attackTimer = 0.0f;
        }

        return turnShouldPass;
    }
    
    private bool HPStrategy(){
        bool turnShouldPass = false;
        foreach(PerHPTurn t in enemiesInPerHPTurn){
            if(t.isAssigned && t.hPThreshold >= enemiesInRoom[t.enemyType][t.enemyIndex].GetHP()){
                turnShouldPass = true;
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
    public float currentAngleStr31;
    private void Melee31(){
        onStrategyExit = OnMelee31Exit;
        str31Timer += Time.deltaTime;
        if(str31RoundCounter >= 3){
            if(str31Timer >= 1.0f){
                str31Timer = 0.0f;
                str31RoundCounter = 0;
                RandomizeAngleStr31();
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

    public void RandomizeAngleStr31()
    {
        float newAngle = Random.Range(0.0f, 119.0f);
        currentAngleStr31 += newAngle * newAngle % 2 == 0 ? 1.0f : -1.0f;

        if (currentAngleStr31 >= 360.0f)
            currentAngleStr31 -= 360.0f;
        else if (currentAngleStr31 < 0.0f)
            currentAngleStr31 += 360.0f;
    }
    #endregion
    #region "Strategy 32"
    private float str32Timer = 0.0f;
    private int str32RoundCounter = 0;
    private void Melee32(){
        onStrategyExit = OnMelee32Exit;
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
        onStrategyExit = OnMelee33Exit;
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
        onStrategyExit = OnMelee34Exit;
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