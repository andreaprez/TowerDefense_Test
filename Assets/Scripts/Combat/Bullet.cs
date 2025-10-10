using TowerDefense.GameFlow;
using TowerDefense.Service;
using UnityEngine;

namespace TowerDefense.Combat
{
    public class Bullet : MonoBehaviour
    {
        private GameFlowService _gameFlowService;
        private TeamTag _teamTag;
        private float _speed;
        private float _lifeTime;
        private float _elapsedTime;
        private int _damage;
        private Transform _target;
        private Vector3 _direction;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
        }

        private void Update()
        {
            if (_gameFlowService.GameState != GameState.Gameplay)
                return;

            if (_elapsedTime >= _lifeTime)
            {
                Destroy(gameObject);
                return;
            }
            _elapsedTime += Time.deltaTime;

            if (_target != null)
                Move();
        }

        public Bullet SetTeam(TeamTag teamTag)
        {
            _teamTag = teamTag;
            return this;
        }

        public Bullet SetSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public Bullet SetLifeTime(float lifeTime)
        {
            _lifeTime = lifeTime;
            return this;
        }

        public Bullet SetDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public Bullet SetTarget(Transform target)
        {
            _target = target;
            _direction = _target.position - transform.position;
            return this;
        }

        private void Move()
        {
            transform.LookAt(_target, Vector3.up);
            transform.position += _direction * (_speed * Time.deltaTime);
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
            Destroy(gameObject);
        }
    }
}