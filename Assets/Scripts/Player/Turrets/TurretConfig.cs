using UnityEngine;

namespace TowerDefense.Player
{
    [CreateAssetMenu(menuName = "Tower Defense/Turret Config")]
    public class TurretConfig : ScriptableObject
    {
        public float Range;
        public float FireRateInSeconds;
        public float BulletSpeed;
        public float BulletLifeTime;
        public int Damage;
    }
}
