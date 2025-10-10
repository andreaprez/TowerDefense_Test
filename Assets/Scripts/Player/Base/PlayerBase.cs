using TowerDefense.Combat;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.Signals;
using TowerDefense.UI;
using UnityEngine;

namespace TowerDefense.Player
{
    public class PlayerBase : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private PlayerBaseConfig _baseConfig;
        [SerializeField] private TeamTag _teamTag;
        [SerializeField] private HealthBar _healthBar;

        private SignalService _signalService;
        private int _hitPoints;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            SetHitPoints(_baseConfig.HitPoints);
        }

        private void SetHitPoints(int value)
        {
            _hitPoints = value;
            _healthBar.UpdateBarFill(_hitPoints, _baseConfig.HitPoints);
        }

        private void Die()
        {
            Destroy(gameObject);
            _signalService.GetSignal<GameEndedSignal>().Send(false);
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
