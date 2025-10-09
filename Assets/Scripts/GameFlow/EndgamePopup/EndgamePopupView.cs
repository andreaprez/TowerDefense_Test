using System;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.GameFlow.EndgamePopup
{
    public class EndgamePopupView : PopupView
    {
        [SerializeField] private Text _messageText;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;

        public event Action MenuButtonPressed;
        public event Action RestartButtonPressed;

        public override void Show()
        {
            _menuButton.onClick.AddListener(OnMenuButtonPressed);
            _restartButton.onClick.AddListener(OnRestartButtonPressed);
            base.Show();
        }

        public override void Hide()
        {
            _menuButton.onClick.RemoveListener(OnMenuButtonPressed);
            _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
            base.Hide();
        }

        public void OnMessageChanged(string message)
        {
            _messageText.text = message;
        }

        private void OnMenuButtonPressed()
        {
            MenuButtonPressed?.Invoke();
        }

        private void OnRestartButtonPressed()
        {
            RestartButtonPressed?.Invoke();
        }
    }
}