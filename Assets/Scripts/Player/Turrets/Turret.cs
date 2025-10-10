using System;
using System.Collections.Generic;
using TowerDefense.Combat;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using UnityEngine;

namespace TowerDefense.Player
{
    public abstract class Turret : MonoBehaviour
    {
        [SerializeField] protected TurretConfig _turretConfig;
        [SerializeField] protected TeamTag _teamTag;
        [SerializeField] protected Bullet _bulletPrefab;
        [SerializeField] protected Transform _bulletSpawnPoint;
        [SerializeField] private SphereCollider _rangeCollider;

        private GameFlowService _gameFlowService;
        private float _elapsedTime;

        protected Dictionary<IDamageReceiver, Transform> _targetsInRange;
        protected Tuple<IDamageReceiver, Transform> _closestTarget;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
            _targetsInRange = new Dictionary<IDamageReceiver, Transform>();
            _rangeCollider.radius = _turretConfig.Range;
        }

        private void Update()
        {
            if (_gameFlowService.GameState != GameState.Gameplay || _turretConfig == null)
                return;

            if (_elapsedTime < _turretConfig.FireRateInSeconds)
            {
                _elapsedTime += Time.deltaTime;
                return;
            }

            if (_targetsInRange.Count == 0)
                return;

            if (_closestTarget?.Item1 == null || _closestTarget?.Item2 == null)
            {
                TryUpdateClosestTarget();
                return;
            }

            _elapsedTime = 0;
            Attack();
        }

        protected abstract void Attack();

        public void OnTriggerEnter(Collider other)
        {
            CheckTriggerToAddAsTarget(other);
        }

        public void OnTriggerExit(Collider other)
        {
            CheckTriggerToRemoveAsTarget(other);
        }

        private void CheckTriggerToAddAsTarget(Collider other)
        {
            other.gameObject.TryGetComponent<IDamageReceiver>(out var damageReceiver);
            if (damageReceiver == null)
                return;
            if (damageReceiver.GetTeamTag() == _teamTag)
                return;

            _targetsInRange.Add(damageReceiver, other.transform);
            TryUpdateClosestTarget();
        }

        private void CheckTriggerToRemoveAsTarget(Collider other)
        {
            if (!_targetsInRange.ContainsValue(other.transform))
                return;

            other.gameObject.TryGetComponent<IDamageReceiver>(out var damageReceiver);
            if (damageReceiver == null)
                return;
            if (damageReceiver.GetTeamTag() == _teamTag)
                return;
            
            _targetsInRange.Remove(damageReceiver);
            TryUpdateClosestTarget();
        }

        private void TryUpdateClosestTarget()
        {
            if (!ValidateTargets())
            {
                _closestTarget = null;
                return;
            }

            var closestDistance = -1f;
            foreach (var target in _targetsInRange)
            {
                var sqrDistance = Vector3.SqrMagnitude(target.Value.position - transform.position);
                if (closestDistance >= 0 && closestDistance < sqrDistance)
                    continue;

                closestDistance = sqrDistance;
                _closestTarget = new Tuple<IDamageReceiver, Transform>(target.Key, target.Value);
            }
        }

        private bool ValidateTargets()
        {
            var targetsToCleanup = new List<IDamageReceiver>();
            foreach (var target in _targetsInRange)
            {
                if (target.Key == null || target.Value == null)
                    targetsToCleanup.Add(target.Key);
            }
            foreach (var target in targetsToCleanup)
            {
                _targetsInRange.Remove(target);
            }

            return _targetsInRange.Count != 0;
        }
    }
}
