using UnityEngine;

namespace TowerDefense.Player
{
    public class TurretRegular : Turret
    {
        protected override void Attack()
        {
            var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, transform);
            bullet.SetTeam(_teamTag)
                .SetSpeed(_turretConfig.BulletSpeed)
                .SetLifeTime(_turretConfig.AttackLifeTime)
                .SetDamage(_turretConfig.Damage)
                .SetTarget(_closestTarget.Item2);
        }
    }
}