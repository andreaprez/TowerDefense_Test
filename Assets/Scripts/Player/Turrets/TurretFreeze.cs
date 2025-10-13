using TowerDefense.Combat;
using UnityEngine;

namespace TowerDefense.Player
{
    public class TurretFreeze : Turret
    {
        [SerializeField] private AreaEffect _areaEffectPrefab;

        protected override void Attack()
        {
            var areaEffect = Instantiate(_areaEffectPrefab, transform.position, Quaternion.identity, transform);
            areaEffect.SetTeam(_teamTag).SetRange(_turretConfig.Range).SetLifeTime(_turretConfig.AttackLifeTime).SetEffectType(CombatEffect.Freeze);
        }
    }
}