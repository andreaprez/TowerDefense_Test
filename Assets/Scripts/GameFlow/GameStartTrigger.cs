using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.GameFlow
{
    public class GameStartTrigger : MonoBehaviour
    {
        private void Start()
        {
            var signalService = ServiceLocator.GetService<SignalService>();
            signalService.GetSignal<GameStartedSignal>().Send();
        }
    }
}