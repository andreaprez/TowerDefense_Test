using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.GameFlow
{
    public class GameInit : MonoBehaviour
    {
        private SignalService _signalService;

        private void Start()
        {
            RegisterServices();

            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<GameStartedSignal>().Send();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService<SignalService>(new SignalService());
            ServiceLocator.RegisterService<GameFlowService>(new GameFlowService());
        }
    }
}