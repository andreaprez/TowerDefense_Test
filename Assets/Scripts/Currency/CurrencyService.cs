using System;
using TowerDefense.GameFlow;
using TowerDefense.Service;
using TowerDefense.Signals;

namespace TowerDefense.Currency
{
    public class CurrencyService : IService
    {
        private readonly CurrencyConfig _currencyConfig;
        private SignalService _signalService;

        public int Coins => _coins;
        private int _coins;

        public CurrencyService(CurrencyConfig currencyConfig)
        {
            _currencyConfig = currencyConfig;
        }

        public void Init()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<GameRestartedSignal>().AddListener(OnGameRestarted);

            _coins = _currencyConfig.InitialCoins;
        }

        public void AddCoins(int amount)
        {
            _coins += amount;
            _signalService.GetSignal<CoinsUpdatedSignal>().Send(_coins);
        }

        public void RemoveCoins(int amount)
        {
            _coins -= amount;
            _coins = Math.Max(_coins, 0);
            _signalService.GetSignal<CoinsUpdatedSignal>().Send(_coins);
        }

        private void OnGameRestarted()
        {
            _coins = _currencyConfig.InitialCoins;
            _signalService.GetSignal<CoinsUpdatedSignal>().Send(_coins);
        }
    }
}