using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class RunSaver{
    public static RunDataManager currentRun = new RunDataManager();
    private static RunDataManager savedRun = new RunDataManager();
    private static HistoryDataManager historyData = new HistoryDataManager();

    private static string RunPath(){
        return Application.persistentDataPath + "/bsSave.save";
    }

    private static string HistoryPath(){
        return Application.persistentDataPath + "/bsHistory.save";
    }

    public static void NewRun(){
        CreateRunFile();

        if (HistoryFileExists())
            LoadHistoryFile();

        //SceneLoaderManager.Instance.LoadNextScene(saveGame.data.actualScene);
    }

    public static void Save(){
        savedRun = currentRun;

        if (currentRun.data.runFinished){
            if (!HistoryFileExists())
                CreateHistoryFile();

            if (currentRun.data.win){
                float time = currentRun.data.time;
                historyData.data.runTimers.Add(time);
                if (time < historyData.data.bestTime)
                    historyData.data.bestTime = time;
            }
            else
                historyData.data.totalDeaths++;

            historyData.data.enemiesKilled += currentRun.data.enemiesKilled;
            historyData.data.bossesKilled += currentRun.data.bossesKilled;
            historyData.data.totalExp += currentRun.data.expObtained;
            historyData.data.totalDamageDealt += currentRun.data.damageDealt;
            historyData.data.actualLevel = currentRun.data.level;
            historyData.data.roomsDiscovered += currentRun.data.roomsDiscovered;

            BinaryFormatter bfHistory = new BinaryFormatter();
            FileStream historyFile = File.OpenWrite(HistoryPath());
            bfHistory.Serialize(historyFile, historyData);
            historyFile.Close();
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(RunPath());
        bf.Serialize(file, savedRun);
        file.Close();
    }
     
    public static void LoadRun(){
        LoadRunFile();
        LoadHistoryFile();
    }

    public static void LoadHistory(){
        LoadHistoryFile();
    }

    public static bool RunFileExists(){
        return File.Exists(RunPath());
    }

    public static bool HistoryFileExists(){
        return File.Exists(HistoryPath());
    }

    private static void CreateRunFile(){
        FillDefaultRunData();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(RunPath());
        bf.Serialize(file, savedRun);
        file.Close();
    }

    private static void LoadRunFile(){
        if(RunFileExists()){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(RunPath(), FileMode.Open);
            currentRun = savedRun = (RunDataManager)bf.Deserialize(file);
            //SceneLoaderManager.Instance.LoadNextScene(saveGame.data.actualScene);
            file.Close();
        }
        else{
            NewRun();
        }
    }

    private static void CreateHistoryFile(){
        FillDefaultHistoryData();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(HistoryPath());
        bf.Serialize(file, historyData);
        file.Close();
    }

    private static void LoadHistoryFile(){
        if(HistoryFileExists()){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(HistoryPath(), FileMode.Open);
            historyData = (HistoryDataManager)bf.Deserialize(file);
            file.Close();
        }
        else{
            CreateHistoryFile();
        }
    }

    public static void ResetHistory(){
        CreateHistoryFile();
    }

    private static void FillDefaultRunData(){
        currentRun.data.damageDealt = 0;
        currentRun.data.damageRecieved = 0;
        currentRun.data.enemiesKilled = 0;
        currentRun.data.bossesKilled = 0;
        currentRun.data.expObtained = 0;
        currentRun.data.time = 0;
        currentRun.data.level = 1;
        currentRun.data.roomsDiscovered = 0;
        currentRun.data.runFinished = false;
        currentRun.data.win = false;

        savedRun = currentRun;
    }

    private static void FillDefaultHistoryData(){
        historyData.data.runTimers = new List<float>();
        historyData.data.bestTime = float.MaxValue;
        historyData.data.enemiesKilled = 0;
        historyData.data.bossesKilled = 0;
        historyData.data.totalExp = 0;
        historyData.data.totalDamageDealt = 0;
        historyData.data.totalDeaths = 0;
        historyData.data.totalAttacks = 0;
        historyData.data.actualLevel = 1;
        historyData.data.roomsDiscovered = 0;
    }

    public static HistoryDataManager.Data GetHistoryData(){
        return historyData.data;
    }
}