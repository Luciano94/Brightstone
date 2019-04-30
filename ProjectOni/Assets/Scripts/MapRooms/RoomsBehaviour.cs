using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsBehaviour : MonoBehaviour{

    [SerializeField]private int enemiesCant = 5;
    [SerializeField]private bool haveMarket = true;
    [SerializeField]private GameObject marketPrefab;
    [SerializeField]private GameObject enemyPrefab;

    private List<GameObject> enemies = null;
    private GameObject market = null;   
    private int enemiesLeft;

    private bool isComplete = false;

    public bool Complete{
        get{return isComplete;}
    }

    private void Start() {
        if(enemiesCant > 0){
            enemiesLeft = enemiesCant;
            enemies = new List<GameObject>();
            for(int i = 0; i < enemiesCant; i++){
                float x = Random.Range(transform.position.x,transform.position.x + 5);
                float y = Random.Range(transform.position.y,transform.position.y + 5);
                Vector3 pos = new Vector3(x,y,transform.position.z);
                enemies.Add( Instantiate(enemyPrefab, pos, transform.rotation));
                enemies[i].GetComponent<EnemyStats>().MyRoom = gameObject;
                enemies[i].SetActive(false);
            }
        }

        if(haveMarket)
            market = Instantiate(marketPrefab, transform.position, transform.rotation);
    }

    public void ActiveEnemies(){
        foreach (GameObject enemy in enemies){
            enemy.SetActive(true);
        }
    }

    public void EnemyDeath(){
        enemiesCant--;
        if(enemiesCant <= 0){
            GetComponent<DoorManager>().DesactiveRoom();
            isComplete = true;
            Debug.Log(enemiesCant);
        }
    }

    public void DesactiveEnemies(){
        foreach (GameObject enemy in enemies){
            enemy.SetActive(true);
        }
    }
}
