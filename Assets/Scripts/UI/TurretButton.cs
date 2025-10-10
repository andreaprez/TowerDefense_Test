using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class TurretButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _costText;

        public void AddListener(UnityAction listener)
        {
            _button.onClick.AddListener(listener);
        }

        public void RemoveListener(UnityAction listener)
        {
            _button.onClick.RemoveListener(listener);
        }

        public void SetImageColor(Color color)
        {
            _button.image.color = color;
        }

        public void SetCostText(string text)
        {
            _costText.SetText(text);
        }
    }
}