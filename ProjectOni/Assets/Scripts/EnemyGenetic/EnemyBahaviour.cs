using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<GameObject> enemies = null;

    private void Update(){
        if(enemies != null){
            

        }
    }

    public void Death(GameObject thisEnemy){
        enemies.Remove(thisEnemy);

        if(enemies.Count <= 0)
            enemies = null;
    }

    public void SetEnemies(List<GameObject> enemies){
        this.enemies = enemies;
    }
}
