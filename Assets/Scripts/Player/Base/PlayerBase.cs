using System.Collections.Generic;
using TowerDefense.Combat;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.Player
{
    public class PlayerBase : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private PlayerBaseConfig _baseConfig;
        [SerializeField] private TeamTag _teamTag;
        [SerializeField] private HealthBarDisplay _healthBar;

        private SignalService _signalService;
        private int _hitPoints;
        private List<CombatEffect> _activeEffects;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<GameRestartedSignal>().AddListener(OnGameRestarted);

            SetHitPoints(_baseConfig.HitPoints);
            _activeEffects = new List<CombatEffect>();
        }

        private void SetHitPoints(int value)
        {
            _hitPoints = value;
            _healthBar.UpdateBarFill(_hitPoints, _baseConfig.HitPoints);
        }

        private void Die()
        {
            _signalService.GetSignal<GameEndedSignal>().Send(false);
            Destroy(gameObject);
        }

        private void OnGameRestarted()
        {
            SetHitPoints(_baseConfig.HitPoints);
            _activeEffects = new List<CombatEffect>();
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
