using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.GameFlow
{
    public class GameInit : MonoBehaviour
    {
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService<SignalService>(new SignalService());
            ServiceLocator.RegisterService<GameFlowService>(new GameFlowService());
        }
    }
}