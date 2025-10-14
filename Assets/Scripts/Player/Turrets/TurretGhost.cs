using TowerDefense.Currency;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.Player
{
    public class TurretGhost : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Color _placeableColor;
        [SerializeField] private Color _notPlaceableColor;
        [SerializeField] private TurretsBuildConfig _turretsBuildConfig;

        private CurrencyService _currencyService;
        private SignalService _signalService;
        private Camera _mainCamera;

        private void Start()
        {
            _currencyService = ServiceLocator.GetService<CurrencyService>();
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<CoinsUpdatedSignal>().AddListener(OnCoinsUpdated);

            _mainCamera = Camera.main;
            SetGhostColor(IsTurretPlaceable(_currencyService.Coins));
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<CoinsUpdatedSignal>().RemoveListener(OnCoinsUpdated);
        }

        private void Update()
        {
            transform.position = GetMousePositionToWorld();
        }

        private void SetGhostColor(bool isPlaceable)
        {
            _meshRenderer.material.color = isPlaceable ? _placeableColor : _notPlaceableColor;
        }

        private Vector3 GetMousePositionToWorld()
        {
            var mousePosition = UnityEngine.Input.mousePosition;
            var mousePositionWorld = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -_mainCamera.transform.position.z));
            return mousePositionWorld;
        }

        private bool IsTurretPlaceable(int coins)
        {
            return coins >= _turretsBuildConfig.TurretCost;
        }

        private void OnCoinsUpdated(int coins)
        {
            SetGhostColor(IsTurretPlaceable(coins));
        }
    }
}