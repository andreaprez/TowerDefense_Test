using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Spawn
{
    [CreateAssetMenu(menuName = "Tower Defense/Fixed Spawn Config")]
    public class FixedSpawnConfig : ScriptableObject
    {
        public List<GameObject> PrefabsToSpawn;
        public int SpawnFrequencyInSeconds;
        public int SpawnQuantity;
        public bool SpawnMultipleInSamePoint;
    }
}
