using System.Collections.Generic;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using UnityEngine;

namespace TowerDefense.Combat
{
    public class AreaEffect : MonoBehaviour
    {
        private GameFlowService _gameFlowService;
        private TeamTag _teamTag;
        private float _lifeTime;
        private float _elapsedTime;
        private CombatEffect _effectType;
        private List<IDamageReceiver> _affectedTargets;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
            _affectedTargets = new List<IDamageReceiver>();
        }

        private void Update()
        {
            if (_gameFlowService.GameState != GameState.Gameplay)
                return;

            if (_elapsedTime >= _lifeTime)
            {
                RemoveEffectOnAllTargets();
                Destroy(gameObject);
                return;
            }
            _elapsedTime += Time.deltaTime;
        }

        public AreaEffect SetTeam(TeamTag teamTag)
        {
            _teamTag = teamTag;
            return this;
        }

        public AreaEffect SetRange(float range)
        {
            transform.localScale = Vector3.one * range * 2;
            return this;
        }
        
        public AreaEffect SetLifeTime(float lifeTime)
        {
            _lifeTime = lifeTime;
            return this;
        }

        public AreaEffect SetEffectType(CombatEffect effectType)
        {
            _effectType = effectType;
            return this;
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckTriggerForEffect(other);
        }

        private void CheckTriggerForEffect(Collider other)
        {
            other.gameObject.TryGetComponent<IDamageReceiver>(out var damageReceiver);
            if (damageReceiver == null)
                return;
            if (damageReceiver.GetTeamTag() == _teamTag)
                return;

            damageReceiver.ApplyEffect(_effectType);
            _affectedTargets.Add(damageReceiver);
        }

        private void RemoveEffectOnAllTargets()
        {
            foreach (var target in _affectedTargets)
            {
                target.RemoveEffect(_effectType);
            }
            _affectedTargets.Clear();
        }
    }
}