using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Camera _targetCamera;

    private void Start()
    {
        _targetCamera = Camera.main;
        ValidateReferences();
    }

    private void ValidateReferences()
    {
        if (_targetCamera == null)
        {
            Debug.LogError("Main camera is not found. Health bars need a camera to follow");
            Destroy(gameObject);
        }
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
