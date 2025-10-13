using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Spawn
{
    [CreateAssetMenu(menuName = "Tower Defense/Waves Spawn Config")]
    public class WavesSpawnConfig : ScriptableObject
    {
        public List<WaveConfig> Waves;
    }
}