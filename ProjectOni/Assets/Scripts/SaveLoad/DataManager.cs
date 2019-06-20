using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ==========================================================================
// This will save the Data of the Run if the player close it before finishing
// ==========================================================================

[System.Serializable]
public class DataManager{
    [System.Serializable]
    public struct Data{
        /*public SceneEnum actualScene;
        public int moneyCount;
        public string waveName;
        public int[] playerUpgrades;
        public int enemyBodies;
        public bool saveCreated;*/
    }

	public static DataManager current;

    public Data data;

	public DataManager(){
        //data.playerUpgrades = new int[(int)Buyable.COUNT];
    }

    public void SetPreparations(){
        /*data.actualScene = (SceneEnum)System.Enum.Parse(typeof(SceneEnum), SceneManager.GetActiveScene().name);

        if (data.actualScene != SceneEnum.StoryboardN1 && data.actualScene != SceneEnum.StoryboardN2 && data.actualScene != SceneEnum.StoryboardN3 && data.actualScene != SceneEnum.StoryboardN4)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            data.moneyCount = player.GetComponent<MoneyHolder>().ActualMoney;
            data.waveName = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().GetActualWaveName();
            data.enemyBodies = BodiesHolder.Instance.GetBodies();

            PlayerMovement3D pMovScript = player.GetComponent<PlayerMovement3D>();

            for (int i = 0; i < (int)Buyable.COUNT; i++)
                data.playerUpgrades[i] = pMovScript.GetUpgradeValue(i);
        }
        else
        {
            data.waveName = "wave 1";
            data.enemyBodies = 0;
        }

        data.saveCreated = true;*/


    }
}