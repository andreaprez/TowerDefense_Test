using System;
using TowerDefense.Input;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class TurretsPanel : MonoBehaviour
    {
        [SerializeField] private Button _regularTurretButton;
        [SerializeField] private Color _selectedColor;

        private SignalService _signalService;
        private Button _selectedTurret;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();

            _regularTurretButton.onClick.AddListener(OnRegularTurretButtonPressed);
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().AddListener(OnToggleSelectionForRegularTurret);

        }

        private void OnDestroy()
        {
            _regularTurretButton.onClick.RemoveListener(OnRegularTurretButtonPressed);
        }

        private void OnRegularTurretButtonPressed()
        {
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().Send();
        }

        private void OnToggleSelectionForRegularTurret()
        {
            _selectedTurret = _selectedTurret == _regularTurretButton ? null : _regularTurretButton;
            _regularTurretButton.image.color = _selectedTurret == _regularTurretButton ? _selectedColor : Color.white;
        }
    }
}