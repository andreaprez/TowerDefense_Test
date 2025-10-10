using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.Input
{
    public class GameplayInputHandler : MonoBehaviour
    {
        private const int TerrainLayer = 3;
        private const int TerrainMask = 1 << TerrainLayer;

        private SignalService _signalService;
        private GameFlowService _gameFlowService;
        private Camera _camera;

        private void Start()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_gameFlowService.GameState != GameState.Gameplay)
                return;

            if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
                ToggleSelectionForRegularTurret();

            if (UnityEngine.Input.GetMouseButtonDown(0))
                PlaceTurret();
        }

        private void ToggleSelectionForRegularTurret()
        {
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().Send();
        }

        private void PlaceTurret()
        {
            var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000f, TerrainMask))
            {
                var mouseWorldPosition = hit.point;
                _signalService.GetSignal<PlacedTurretSignal>().Send(mouseWorldPosition);
            }
        }
    }
}