using System;

namespace Game.Configs.LevelConfigs
{
    [Serializable]
    public struct EnemySpawnData
    {
        public int Id;
        public float Hp;
        public float Damage;
        public bool IsBoss;
        public float BossTime;
    }
}