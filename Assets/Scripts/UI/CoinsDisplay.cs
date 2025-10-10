using TMPro;
using TowerDefense.Currency;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.UI
{
    public class CoinsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsText;

        private SignalService _signalService;

        private void Start()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<CoinsUpdatedSignal>().AddListener(OnCoinsUpdated);

            var currencyService = ServiceLocator.GetService<CurrencyService>();
            OnCoinsUpdated(currencyService.Coins);
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<CoinsUpdatedSignal>().RemoveListener(OnCoinsUpdated);
        }

        private void OnCoinsUpdated(int coins)
        {
            _coinsText.SetText(coins.ToString());
        }
    }
}