using UnityEngine;

namespace TowerDefense.Player
{
    [CreateAssetMenu(menuName = "Tower Defense/Turret Prefabs Collection")]
    public class TurretPrefabsCollection : ScriptableObject
    {
        public Turret RegularTurret;
    }
}