using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Utils
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private int _poolSize;

        private List<GameObject> _pooledObjects;

        private void Start()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            _pooledObjects = new List<GameObject>();
            for (var i = 0; i < _poolSize; i++)
            {
                var poolObject = Instantiate(_objectPrefab, transform);
                poolObject.SetActive(false);
                _pooledObjects.Add(poolObject);
            }
        }

        public GameObject Get()
        {
            for (var i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    _pooledObjects[i].SetActive(true);
                    return _pooledObjects[i];
                }
            }

            return null;
        }

        public void Release(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}