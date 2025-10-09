using TowerDefense.Service;
using UnityEngine;

namespace TowerDefense.GameFlow
{
    public class GameInit : MonoBehaviour
    {
        private GameFlowService _gameFlowService;

        private void Start()
        {
            RegisterServices();

            _gameFlowService = ServiceLocator.GetService<GameFlowService>();
            _gameFlowService.StartGame();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService<GameFlowService>(new GameFlowService());
        }
    }
}