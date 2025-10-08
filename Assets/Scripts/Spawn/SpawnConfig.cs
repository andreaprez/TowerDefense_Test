using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Spawn
{
    [CreateAssetMenu(menuName = "Tower Defense/Spawn Config")]
    public class SpawnConfig : ScriptableObject
    {
        public List<GameObject> PrefabsToSpawn;
        public int SpawnFrequencyInSeconds;
        public int SpawnQuantity;
        public bool SpawnMultipleInSamePoint;
    }
}
