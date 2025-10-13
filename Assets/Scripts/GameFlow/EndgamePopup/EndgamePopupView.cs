using System;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.GameFlow.EndgamePopup
{
    public class EndgamePopupView : PopupView
    {
        [SerializeField] private Text _messageText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        public event Action RestartButtonPressed;
        public event Action MenuButtonPressed;

        public override void Show()
        {
            _restartButton.onClick.AddListener(OnRestartButtonPressed);
            _menuButton.onClick.AddListener(OnMenuButtonPressed);
            base.Show();
        }

        public override void Hide()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
            _menuButton.onClick.RemoveListener(OnMenuButtonPressed);
            base.Hide();
        }

        public void OnMessageChanged(string message)
        {
            _messageText.text = message;
        }

        private void OnRestartButtonPressed()
        {
            RestartButtonPressed?.Invoke();
        }

        private void OnMenuButtonPressed()
        {
            MenuButtonPressed?.Invoke();
        }
    }
}