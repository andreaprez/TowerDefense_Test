using System;
using System.Collections.Generic;

namespace TowerDefense.Spawn
{
    [Serializable]
    public class WaveConfig
    {
        public List<WaveEnemyConfig> EnemyGroups;
    }
}