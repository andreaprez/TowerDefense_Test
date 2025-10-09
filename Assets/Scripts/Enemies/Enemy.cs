using TowerDefense.Combat;
using TowerDefense.Scene;
using TowerDefense.UI;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private HealthBar _healthBar;

        protected TeamTag _teamTag;
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
            _teamTag = TeamTag.Enemy;
            SetHitPoints(_enemyConfig.HitPoints);
            _damage = _enemyConfig.Damage;
            _speed = _enemyConfig.Speed;
            _targetPosition = TargetReferencesHolder.Instance.PlayerBase.position;
        }

        private void SetHitPoints(int value)
        {
            _hitPoints = value;
            _healthBar.UpdateBarFill(_hitPoints, _enemyConfig.HitPoints);
        }

        protected abstract void Move();

        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        public void ApplyDamage(int damagePoints)
        {
            SetHitPoints(_hitPoints - damagePoints);
            if (_hitPoints <= 0)
                Die();
        }

        public TeamTag GetTeamTag()
        {
            return _teamTag;
        }
    }
}
