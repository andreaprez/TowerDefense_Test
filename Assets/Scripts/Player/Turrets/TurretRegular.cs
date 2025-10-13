using TowerDefense.Combat;
using TowerDefense.Utils;
using UnityEngine;

namespace TowerDefense.Player
{
    public class TurretRegular : Turret
    {
        protected override void Attack()
        {
            var bullet = PoolHandler.Instance.BulletsPool.Get();
            if (bullet == null)
                return;

            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.GetComponent<Bullet>()
                .SetTeam(_teamTag)
                .SetSpeed(_turretConfig.BulletSpeed)
                .SetLifeTime(_turretConfig.AttackLifeTime)
                .SetDamage(_turretConfig.Damage)
                .SetTarget(_closestTarget.Item2);
        }
    }
}