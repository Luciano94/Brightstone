using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsBehaviour : MonoBehaviour{

    [SerializeField]private int enemiesCant = 5;
    [SerializeField]private bool haveMarket = false;
    [SerializeField]private GameObject marketPrefab;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private Transform[] enemySpawns;

    private Vector3 pos;

    private List<GameObject> enemies = null;
    private GameObject market = null;   
    private int enemiesLeft;

    private bool isComplete = false;

    public bool Complete{
        get{return isComplete;}
    }

    public bool HaveMarket{
        get{return haveMarket;}
    }

    private void Start() {
        enemiesCant = Random.Range(0, enemiesCant);
        if(enemiesCant > 0){
            enemiesLeft = enemiesCant;
            enemies = new List<GameObject>();
            for(int i = 0; i < enemiesCant; i++){
                pos = enemySpawns[Random.Range(0, enemySpawns.Length)].position;
                enemies.Add( Instantiate(enemyPrefab, pos, transform.rotation));
                enemies[i].GetComponent<EnemyStats>().MyRoom = gameObject;
                enemies[i].SetActive(false);
            }
        }else{
            haveMarket = true;
        }

        if(haveMarket){
            market = Instantiate(marketPrefab, transform.position, transform.rotation);
            market.transform.position += new Vector3(0,0,-1);
            EnemyDeath();
        }
    }

    public void ActiveEnemies(){
        if(!haveMarket){
            foreach (GameObject enemy in enemies){
                enemy.SetActive(true);
            }
        }else{
            SwitchMarket();
        }
    }

    public void SwitchMarket(){
        if(market.layer == 11){
            market.layer = 16;
        }else{
            market.layer = 11;
        }
    }

    public void EnemyDeath(){
        enemiesCant--;
        if(enemiesCant <= 0){
            GetComponent<NodeExits>().OpenDoors();
            isComplete = true;
        }
    }

    public void DesactiveEnemies(){
        foreach (GameObject enemy in enemies){
            enemy.SetActive(true);
        }
    }
}
