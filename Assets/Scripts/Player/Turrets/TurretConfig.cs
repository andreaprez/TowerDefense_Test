using UnityEngine;

namespace TowerDefense.Player
{
    [CreateAssetMenu(menuName = "Tower Defense/Turret Config")]
    public class TurretConfig : ScriptableObject
    {
        public float Range;
        public float AttackRateInSeconds;
        public float AttackLifeTime;
        public float BulletSpeed;
        public int Damage;
    }
}
