using System;
using TowerDefense.Enemies;

namespace TowerDefense.Spawn
{
    [Serializable]
    public class WaveEnemyConfig
    {
        public Enemy Prefab;
        public int Quantity;
    }
}