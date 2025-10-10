using UnityEngine;

namespace TowerDefense.Player
{
    [CreateAssetMenu(menuName = "Tower Defense/Turrets Build Config")]
    public class TurretsBuildConfig : ScriptableObject
    {
        public int TurretCost;
        public Turret RegularTurretPrefab;
    }
}