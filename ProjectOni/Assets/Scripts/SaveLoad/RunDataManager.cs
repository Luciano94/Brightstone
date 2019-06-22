// =========================================
// This will save the Data of the actual Run
// =========================================

[System.Serializable]
public class RunDataManager{
    [System.Serializable]
    public struct Data{
        public uint damageDealt;
        public uint damageRecieved;
        public uint timesParried;
        public uint goodParry;
        public uint enemiesKilled;
        public uint bossesKilled;
        public uint expObtained;
        public float time;
        public ushort level;
        public ushort roomsDiscovered;
        public bool runFinished;
        public bool win;
    }

	public static RunDataManager current;

    public Data data;
}