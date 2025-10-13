using TowerDefense.Combat;
using TowerDefense.GameFlow;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public class Creep : Enemy
    {
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
            _hitPoints = 0;
            _signalService.GetSignal<EnemyDiedSignal>().Send(this);
            Destroy(gameObject);
        }
    }
}
