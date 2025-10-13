using TowerDefense.GameFlow;
using TowerDefense.Input;
using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

namespace TowerDefense.Player
{
    public class TurretsPanel : MonoBehaviour
    {
        [SerializeField] private TurretsBuildConfig _turretsBuildConfig;
        [SerializeField] private TurretButton _regularTurretButton;
        [SerializeField] private TurretButton _freezeTurretButton;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private GameObject _buttonsHolder;

        private SignalService _signalService;
        private TurretButton _selectedTurret;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().AddListener(OnToggleSelectionForTurret);
            _signalService.GetSignal<GameStartedSignal>().AddListener(OnGameStarted);
            _signalService.GetSignal<GameEndedSignal>().AddListener(OnGameEnded);
            _signalService.GetSignal<GameRestartedSignal>().AddListener(OnGameRestarted);
            SetupRegularTurretButton();
            SetupFreezeTurretButton();
        }

        private void SetupRegularTurretButton()
        {
            _regularTurretButton.SetCostText(_turretsBuildConfig.TurretCost.ToString());
            _regularTurretButton.AddListener(OnRegularTurretButtonPressed);
        }

        private void SetupFreezeTurretButton()
        {
            _freezeTurretButton.SetCostText(_turretsBuildConfig.TurretCost.ToString());
            _freezeTurretButton.AddListener(OnFreezeTurretButtonPressed);
        }

        private void OnDestroy()
        {
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().RemoveListener(OnToggleSelectionForTurret);
            _regularTurretButton.RemoveListener(OnRegularTurretButtonPressed);
            _freezeTurretButton.RemoveListener(OnFreezeTurretButtonPressed);
        }

        private void OnRegularTurretButtonPressed()
        {
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().Send(TurretType.Regular);
        }

        private void OnFreezeTurretButtonPressed()
        {
            _signalService.GetSignal<ToggledSelectionForTurretSignal>().Send(TurretType.Freeze);
        }

        private void OnToggleSelectionForTurret(TurretType turretType)
        {
            _selectedTurret?.SetImageColor(Color.white);

            TurretButton turretButton = null;
            switch (turretType)
            {
                case TurretType.Regular:
                    turretButton = _regularTurretButton;
                    break;
                case TurretType.Freeze:
                    turretButton = _freezeTurretButton;
                    break;
            }
            _selectedTurret = _selectedTurret == turretButton ? null : turretButton;
            _selectedTurret?.SetImageColor(_selectedTurret == turretButton ? _selectedColor : Color.white);
        }

        private void OnGameStarted()
        {
            _buttonsHolder.SetActive(true);
        }

        private void OnGameEnded(bool isWin)
        {
            _buttonsHolder.SetActive(false);
        }

        private void OnGameRestarted()
        {
            if (_selectedTurret)
            {
                _selectedTurret.SetImageColor(Color.white);
                _selectedTurret = null;
            }
            _buttonsHolder.SetActive(true);
        }
    }
}