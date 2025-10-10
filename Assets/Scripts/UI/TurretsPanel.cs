using TowerDefense.Input;
using TowerDefense.Player;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.UI
{
    public class TurretsPanel : MonoBehaviour
    {
        [SerializeField] private TurretsBuildConfig _turretsBuildConfig;
        [SerializeField] private TurretButton _regularTurretButton;
        [SerializeField] private TurretButton _freezeTurretButton;
        [SerializeField] private Color _selectedColor;

        private SignalService _signalService;
        private TurretButton _selectedTurret;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            SetupRegularTurretButton();
            SetupFreezeTurretButton();
        }

        private void SetupRegularTurretButton()
        {
            _regularTurretButton.SetCostText(_turretsBuildConfig.TurretCost.ToString());
            _regularTurretButton.AddListener(OnRegularTurretButtonPressed);
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().AddListener(OnToggleSelectionForRegularTurret);
        }

        private void SetupFreezeTurretButton()
        {
            _freezeTurretButton.SetCostText(_turretsBuildConfig.TurretCost.ToString());
            _freezeTurretButton.AddListener(OnFreezeTurretButtonPressed);
            _signalService.GetSignal<ToggledSelectionForFreezeTurretSignal>().AddListener(OnToggleSelectionForFreezeTurret);
        }

        private void OnDestroy()
        {
            _regularTurretButton.RemoveListener(OnRegularTurretButtonPressed);
            _freezeTurretButton.RemoveListener(OnFreezeTurretButtonPressed);
        }

        private void OnRegularTurretButtonPressed()
        {
            _signalService.GetSignal<ToggledSelectionForRegularTurretSignal>().Send();
        }

        private void OnFreezeTurretButtonPressed()
        {
            _signalService.GetSignal<ToggledSelectionForFreezeTurretSignal>().Send();
        }

        private void OnToggleSelectionForRegularTurret()
        {
            _selectedTurret = _selectedTurret == _regularTurretButton ? null : _regularTurretButton;
            _regularTurretButton.SetImageColor(_selectedTurret == _regularTurretButton ? _selectedColor : Color.white);
        }

        private void OnToggleSelectionForFreezeTurret()
        {
            _selectedTurret = _selectedTurret == _freezeTurretButton ? null : _freezeTurretButton;
            _freezeTurretButton.SetImageColor(_selectedTurret == _freezeTurretButton ? _selectedColor : Color.white);
        }
    }
}