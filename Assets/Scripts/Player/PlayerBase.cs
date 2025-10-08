using TowerDefense.Combat;
using UnityEngine;

namespace TowerDefense.Player
{
    public class PlayerBase : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private PlayerBaseConfig _baseConfig;
        [SerializeField] private TeamTag _teamTag;

        [SerializeField] private int _hitPoints;

        private void Start()
        {
            ValidateReferences();
            Initialize();
        }

        private void ValidateReferences()
        {
            if (_baseConfig == null)
            {
                Debug.LogError($"Player Base configuration is null. Please reference a PlayerBaseConfig in PlayerBase prefab");
                Destroy(this);
            }
        }

        private void Initialize()
        {
            _hitPoints = _baseConfig.HitPoints;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public void ApplyDamage(int damagePoints)
        {
            _hitPoints -= damagePoints;
            if (_hitPoints <= 0)
                Die();
        }

        public TeamTag GetTeamTag()
        {
            return _teamTag;
        }
    }
}
