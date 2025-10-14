using UnityEngine;

namespace TowerDefense.Utils
{
    public class PoolHandler : MonoBehaviour
    {
        [SerializeField] private ObjectPool _smallCreepsPool;
        [SerializeField] private ObjectPool _bigCreepsPool;
        [SerializeField] private ObjectPool _bulletsPool;

        public static PoolHandler Instance => _instance;
        private static PoolHandler _instance;

        public ObjectPool SmallCreepsPool => _smallCreepsPool;
        public ObjectPool BigCreepsPool => _bigCreepsPool;
        public ObjectPool BulletsPool => _bulletsPool;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Destroy(gameObject);
        }
    }
}