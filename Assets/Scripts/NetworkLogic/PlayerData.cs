using Fusion;

namespace NetworkLogic
{
    public struct PlayerData : INetworkStruct
    {
        [Networked, Capacity(24)] public string Name { get => default; set {} }
        public PlayerRef PlayerRef { get; }
        public int Kills { get; }
        public int Deaths { get; }
        public int LastKillTick { get; }
        public int StatisticPosition { get; }
        public bool IsAlive { get; }
        public bool IsConnected { get; }

        public void SetBaseInfo(string name)
        {
            Name = name;
        }
    }
}