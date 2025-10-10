using TowerDefense.Combat;
using TowerDefense.Currency;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.UI;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private HealthBarDisplay _healthBar;

        protected GameFlowService _gameFlowService;
        private CurrencyService _currencyService;
        protected TeamTag _teamTag;
        protected int _hitPoints;
        protected int _damage;
        protected float _speed;
        protected Vector3 _targetPosition;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
            _currencyService = ServiceLocator.GetService<CurrencyService>();
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

        protected virtual void Die()
        {
            _currencyService.AddCoins(_enemyConfig.DeathReward);
            Destroy(gameObject);
        }

        protected bool IsAlive()
        {
            return _hitPoints > 0;
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
