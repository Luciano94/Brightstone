using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsBehaviour : MonoBehaviour{

    [SerializeField]private int enemiesCant = 5;
    [SerializeField]private bool haveMarket = false;
    [SerializeField]private GameObject marketPrefab;
    [SerializeField]private GameObject bossPrefab;
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private Transform[] enemySpawns;

    private Node node;

    private Vector3 pos;

    private List<GameObject> enemies = null;
    private GameObject market = null;   
    private int enemiesLeft;

    private bool isComplete = false;

    public bool Complete{
        get{return isComplete;}
    }

    public Node Node{
        set{
            node = value;
            setRoom();
        }
    }

    public bool HaveMarket{
        get{return haveMarket;}
    }

    private void setRoom() {
        enemies = new List<GameObject>();

        switch(node.Behaviour){
            case NodeBehaviour.Normal:
                enemiesCant = Random.Range(1, enemiesCant);
                enemiesLeft = enemiesCant;
                for(int i = 0; i < enemiesCant; i++){
                    pos = enemySpawns[Random.Range(0, enemySpawns.Length)].position;
                    enemies.Add( Instantiate(enemyPrefab, pos, transform.rotation));
                    enemies[i].GetComponent<EnemyStats>().MyRoom = gameObject;
                    enemies[i].SetActive(false);
                }
            break;
            case NodeBehaviour.Market:
                haveMarket = true;
                market = Instantiate(marketPrefab, transform.position, transform.rotation);
                market.transform.position += new Vector3(0,0,-1);
                isComplete = true;
            break;
            case NodeBehaviour.MediumBoss:
            break;
            case NodeBehaviour.Boss:
                enemiesLeft = 1;
                pos = enemySpawns[Random.Range(0, enemySpawns.Length)].position;
                enemies.Add( Instantiate(bossPrefab, pos, transform.rotation));
                enemies[0].GetComponent<BossStats>().MyRoom = gameObject;
                enemies[0].SetActive(false);
                GameManager.Instance.SetBoss = enemies[0];
            break;
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
