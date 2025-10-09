using TowerDefense.Combat;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public class Creep : Enemy
    {
        private void Update()
        {
            if (_hitPoints > 0)
                Move();
        }

        protected override void Move()
        {
            transform.LookAt(_targetPosition, Vector3.up);
        
            var direction = _targetPosition - transform.position;
            transform.position += direction.normalized * (_speed * Time.deltaTime);
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
            Destroy(gameObject);
        }
    }
}
