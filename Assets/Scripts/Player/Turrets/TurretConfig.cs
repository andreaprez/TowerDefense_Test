using UnityEngine;

namespace TowerDefense.Player
{
    [CreateAssetMenu(menuName = "Tower Defense/Turret Config")]
    public class TurretConfig : ScriptableObject
    {
        public float FireRateInSeconds;
        public int Damage;
    }
}
