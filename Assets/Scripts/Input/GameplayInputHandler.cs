using TowerDefense.GameFlow;
using TowerDefense.Player;
using TowerDefense.Scene;
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
        private SceneLoadingService _sceneLoadingService;
        private Camera _camera;

        private void Start()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
            _sceneLoadingService = ServiceLocator.GetService<SceneLoadingService>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_gameFlowService.GameState != GameState.Gameplay)
                return;

            if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
                ToggleSelectionForTurret(TurretType.Regular);

            if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
                ToggleSelectionForTurret(TurretType.Freeze);

            if (UnityEngine.Input.GetMouseButtonDown(0))
                TryPlaceTurret();
            
            if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
                GoToStartMenu();
        }

        private void ToggleSelectionForTurret(TurretType turretType)
        {
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().Send(turretType);
        }

        private void TryPlaceTurret()
        {
            var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000f, TerrainMask))
            {
                var mouseWorldPosition = hit.point;
                _signalService.GetSignal<AttemptedTurretPlacementSignal>().Send(mouseWorldPosition);
            }
        }

        private void GoToStartMenu()
        {
            _sceneLoadingService.LoadStartMenuScene();
        }
    }
}