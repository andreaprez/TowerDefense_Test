using TowerDefense.Combat;
using TowerDefense.GameFlow;
using TowerDefense.Utils;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public class Creep : Enemy
    {
        [SerializeField] private EnemyType _enemyType;

        public override EnemyType GetEnemyType()
        {
            return _enemyType;
        }

        protected override void ReleaseFromPool()
        {
            switch (GetEnemyType())
            {
                case EnemyType.SmallCreep:
                    PoolHandler.Instance.SmallCreepsPool.Release(gameObject);
                    break;
                case EnemyType.BigCreep:
                    PoolHandler.Instance.BigCreepsPool.Release(gameObject);
                    break;
            }
        }

        private void Update()
        {
            if (_gameFlowService.GameState != GameState.Gameplay || !IsAlive())
                return;

            Move();
        }

        private void Move()
        {
            transform.LookAt(_targetPosition, Vector3.up);

            var direction = _targetPosition - transform.position;
            var speed = _activeEffects.Contains(CombatEffect.Freeze) ? _speed * 0.5f : _speed;
            transform.position += direction.normalized * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckTriggerForDamage(other);
        }

        private void CheckTriggerForDamage(Collider other)
        {
            other.gameObject.TryGetComponent<IDamageReceiver>(out var damageReceiver);
            if (damageReceiver == null)
                return;
            if (damageReceiver.GetTeamTag() == _teamTag)
                return;

            damageReceiver.ApplyDamage(_damage);
            AutoDestroy();
        }

        private void AutoDestroy()
        {
            SetHitPoints(0);
            _signalService.GetSignal<EnemyDiedSignal>().Send(this);
            ReleaseFromPool();
        }
    }
}
