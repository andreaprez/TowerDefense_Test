using System.Collections.Generic;
using TowerDefense.Combat;
using TowerDefense.Currency;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.Signals;
using TowerDefense.UI;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private HealthBarDisplay _healthBar;

        private CurrencyService _currencyService;
        protected SignalService _signalService;
        protected GameFlowService _gameFlowService;
        protected TeamTag _teamTag;
        protected int _hitPoints;
        protected int _damage;
        protected float _speed;
        protected Vector3 _targetPosition;
        protected List<CombatEffect> _activeEffects;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _currencyService = ServiceLocator.GetService<CurrencyService>();
            _signalService = ServiceLocator.GetService<SignalService>();
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
            _teamTag = TeamTag.Enemy;
            SetHitPoints(_enemyConfig.HitPoints);
            _damage = _enemyConfig.Damage;
            _speed = _enemyConfig.Speed;
            _targetPosition = TargetReferencesHolder.Instance.PlayerBase.position;
            _activeEffects = new List<CombatEffect>();
        }

        private void SetHitPoints(int value)
        {
            _hitPoints = value;
            _healthBar.UpdateBarFill(_hitPoints, _enemyConfig.HitPoints);
        }

        protected virtual void Die()
        {
            _currencyService.AddCoins(_enemyConfig.DeathReward);
            _signalService.GetSignal<EnemyDiedSignal>().Send(this);
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

        public void ApplyEffect(CombatEffect effectType)
        {
            if (!_activeEffects.Contains(effectType))
                _activeEffects.Add(effectType);
        }

        public void RemoveEffect(CombatEffect effectType)
        {
            if (_activeEffects.Contains(effectType))
                _activeEffects.Remove(effectType);
        }

        public TeamTag GetTeamTag()
        {
            return _teamTag;
        }
    }
}
