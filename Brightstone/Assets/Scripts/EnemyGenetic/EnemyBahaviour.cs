﻿using System.Collections;
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

    [SerializeField] private int maxWarriorsAtking;

    private List<List<EnemyBase>> enemies;
    private int enemiesLeft = 0;

    private bool enemyAdded = false;

    void Awake(){
        enemies = new List<List<EnemyBase>>();
        for (int i = 0; i < (int)EnemyType.Count; i++)
            enemies.Add(new List<EnemyBase>());
    }

    private void LateUpdate(){
        if(enemyAdded){
            enemyAdded = false;
            UpdateStrategy();
        }
    }

    public void AddEnemyToBehaviour(GameObject enemy){
        enemiesLeft++;
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        enemies[(int)enemyBase.GetEnemyType()].Add(enemyBase);

        enemyAdded = true;
    }

    public void Death(GameObject thisEnemy){
        EnemyBase enemyBase = thisEnemy.GetComponent<EnemyBase>();
        enemies[(int)enemyBase.GetEnemyType()].Remove(enemyBase);

        enemiesLeft--;

        UpdateStrategy();
    }
    
    public void UpdateStrategy(){
        if(enemiesLeft > 0){
            int countChasing = 0;
            
            foreach (EnemyBase enemy in enemies[(int)EnemyType.Warrior]){
                if (!enemy.IsWaiting()) countChasing++;
                
            }

            if (countChasing < maxWarriorsAtking || countChasing == enemies[(int)EnemyType.Warrior].Count)
                foreach (EnemyBase enemy in enemies[(int)EnemyType.Warrior])
                    if (enemy.IsWaiting() && countChasing < maxWarriorsAtking){
                        countChasing++;
                        enemy.Chase();
                    }

            if (enemies[(int)EnemyType.Boss].Count > 0)
                enemies[(int)EnemyType.Boss][0].Chase();
        }
    }
}