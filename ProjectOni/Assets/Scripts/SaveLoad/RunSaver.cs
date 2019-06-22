using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
 
public static class RunSaver{
    public static RunDataManager currentRun = new RunDataManager();
    private static RunDataManager savedRun = new RunDataManager();
    private static HistoryDataManager historyData = new HistoryDataManager();

    public static void NewGame(){
        FillDefaultRunData();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/bsSave.save");
        bf.Serialize(file, savedRun);
        file.Close();

        //SceneLoaderManager.Instance.LoadNextScene(saveGame.data.actualScene);
    }

    public static void Save(){
        savedRun = currentRun;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite (Application.persistentDataPath + "/bsSave.save");
        bf.Serialize(file, savedRun);
        file.Close();
    }
     
    public static void Load(){
        if(FileExists()){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/bsSave.save", FileMode.Open);
            Debug.Log(Application.persistentDataPath + "/bsSave.save");
            currentRun = savedRun = (RunDataManager)bf.Deserialize(file);
            Debug.Log("Reached here too");
            //SceneLoaderManager.Instance.LoadNextScene(saveGame.data.actualScene);
            file.Close();
        }
        else{
            NewGame();
        }
    }

    public static bool FileExists(){
        return File.Exists(Application.persistentDataPath + "/bsSave.save");
    }

    private static void FillDefaultRunData(){
        currentRun.data.damageDealt = 0;
        currentRun.data.damageRecieved = 0;
        currentRun.data.timesParried = 0;
        currentRun.data.goodParry = 0;
        currentRun.data.enemiesKilled = 0;
        currentRun.data.bossesKilled = 0;
        currentRun.data.expObtained = 0;
        currentRun.data.time = 0;
        currentRun.data.win = false;

        savedRun = currentRun;
    }

    private static void FillDefaultHistoryData(){

    }
}