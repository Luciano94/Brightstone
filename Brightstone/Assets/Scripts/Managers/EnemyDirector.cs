using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    private static EnemyDirector instance;

    public static EnemyDirector Instance{
        get {
            instance = FindObjectOfType<EnemyDirector>();
            if(instance == null){
                GameObject go = new GameObject("EnemyDirector");
                instance = go.AddComponent<EnemyDirector>();
            }
            return instance;
        }
    }

    private const int MIN_VALUE = 1;
    private const int MAX_VALUE = 10; 


    private int difficultSetting = 0;
    public int minDifficultSetting;
    public int maxDifficultSetting;

    private float oneEnemyTime;
    private float controlTime;
    private int controlEnemies;
    private float firstTime;
    public int roomIndex = 0;


    public void startFirstTime(){
        roomIndex = 1;
        difficultSetting = minDifficultSetting;
        firstTime = Time.time;
    }

    public void stopFirstTime(){
        oneEnemyTime = Time.time - firstTime;
    }

    public void startControlTime(int enemyQuant){
        roomIndex++;
        firstTime = Time.time;
        controlEnemies = enemyQuant;
    }

    public void stopControlTime(){
        controlTime = Time.time - firstTime;
    }

    public int getMinDifficultValue(){
        if(roomIndex == 0){
            return 1;
        }
        if(controlTime > (oneEnemyTime * controlEnemies)){
            minDifficultSetting++;
        }else{
            minDifficultSetting --;
        }
        minDifficultSetting = (int)Mathf.Clamp(minDifficultSetting, MIN_VALUE, difficultSetting*0.5f);
        return Random.Range(roomIndex, roomIndex+minDifficultSetting);
    }

    public int getMaxDifficultValue(){
        if(roomIndex == 0){
            return 1;
        }
        if(controlTime > (oneEnemyTime * controlEnemies)){
            maxDifficultSetting++;
            difficultSetting++;
        }
        else{
            maxDifficultSetting--;
            difficultSetting--;
        }
        maxDifficultSetting = (int)Mathf.Clamp(maxDifficultSetting, MAX_VALUE, difficultSetting*0.5f);
        difficultSetting = Mathf.Clamp(difficultSetting, minDifficultSetting, maxDifficultSetting);
        return Random.Range(difficultSetting, difficultSetting+maxDifficultSetting);
    }
}
