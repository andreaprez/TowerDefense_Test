using UnityEngine;

namespace TowerDefense.Enemies
{
    [CreateAssetMenu(menuName = "Tower Defense/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        public int HitPoints;
        public int Damage;
        public float Speed;
        public int DeathReward;
    }
}
