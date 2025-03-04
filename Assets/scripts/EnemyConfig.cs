using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyConfig", fileName = "EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public List<EnemyData> Enemies;
    public Enemy prefab;
}