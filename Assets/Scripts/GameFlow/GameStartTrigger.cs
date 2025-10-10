using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.GameFlow
{
    public class GameStartTrigger : MonoBehaviour
    {
        private SignalService _signalService;

        private void Start()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<GameStartedSignal>().Send();
        }
    }
}