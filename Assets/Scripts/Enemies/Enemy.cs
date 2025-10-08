using TowerDefense.Scene;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;

        protected int _hitPoints;
        protected int _damage;
        protected float _speed;
        protected Vector3 _targetPosition;

        private void Start()
        {
            ValidateReferences();
            Initialize();
        }

        private void ValidateReferences()
        {
            if (_enemyConfig == null)
            {
                Debug.LogError($"Enemy configuration is null. Please reference an EnemyConfig in {gameObject.name} prefab");
                Destroy(this);
            }
        }

        protected virtual void Initialize()
        {
            _hitPoints = _enemyConfig.HitPoints;
            _damage = _enemyConfig.Damage;
            _speed = _enemyConfig.Speed;
            _targetPosition = TargetReferencesHolder.Instance.PlayerBase.position;
        }

        protected virtual void Update()
        {
            if (_hitPoints <= 0)
                Die();
        }

        protected abstract void Move();

        protected virtual void Die()
        {
            Destroy(this);
        }
    }
}
