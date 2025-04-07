using System;
using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Game.Configs.EnemyConfigs
{
    [CreateAssetMenu(menuName = "Configs/EnemiesConfig", fileName = "EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        [Serializable]
        public struct EnemyViewData
        {
            public int Id;
            public string Name;
            public float DamageTime;
            public Transform Prefab;
            public ParticleSystemWrapper PrefabBlood;
            public Vector3 Position;
            public Vector3 Scale;
        }

        public Enemy EnemyPrefab;
        public List<EnemyViewData> Enemies;

        public EnemyViewData GetEnemy(int id)
        {
            foreach (var enemyData in Enemies)
            {
                if (enemyData.Id == id) return enemyData;
            }

            Debug.LogError($"Not found enemy with id {id}");
            return default;
        }
    }
}