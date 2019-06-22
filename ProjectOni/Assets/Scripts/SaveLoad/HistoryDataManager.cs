using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// =====================================================
// This will save the Data of ALL the Runs of the Player
// =====================================================

[System.Serializable]
public class HistoryDataManager{
    [System.Serializable]
    public struct Data{
        public List<float> runTimers;
        public ushort bestTime;
        public uint timesParried;
        public uint goodParry;
        public uint enemiesKilled;
        public uint bossesKilled;
        public uint totalExp;
        public ushort totalDeaths;
        public ushort totalAttacks;
        public ushort actualLevel;
        public ushort roomsDiscovered;
    }

	public static DataManager current;

    public Data data;

	public HistoryDataManager(){
        
    }
}