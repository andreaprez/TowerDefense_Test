using TowerDefense.Currency;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.GameFlow
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField] private CurrencyConfig _currencyConfig;

        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService<SignalService>(new SignalService());
            ServiceLocator.RegisterService<GameFlowService>(new GameFlowService());
            ServiceLocator.RegisterService<CurrencyService>(new CurrencyService(_currencyConfig));
        }
    }
}