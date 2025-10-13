using UnityEngine;

namespace TowerDefense.Combat
{
    public class TargetReferencesHolder : MonoBehaviour
    {
        public static TargetReferencesHolder Instance => _instance;
        private static TargetReferencesHolder _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Destroy(gameObject);
        }

        public Transform PlayerBase;
    }
}