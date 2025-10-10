using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class HealthBarDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private Camera _targetCamera;

        private void Start()
        {
            _targetCamera = Camera.main;
        }

        private void Update()
        {
            var cameraPosition = _targetCamera.transform.position;
            var targetPosition = new Vector3(transform.position.x, cameraPosition.y, cameraPosition.z);
            transform.LookAt(targetPosition);
        }

        public void UpdateBarFill(float currentValue, float maxValue)
        {
            _slider.value = currentValue / maxValue;
        }
    }
}
