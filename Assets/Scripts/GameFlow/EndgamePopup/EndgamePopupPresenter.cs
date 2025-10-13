using TowerDefense.Scene;
using TowerDefense.Service;
using TowerDefense.Signals;
using TowerDefense.UI;

namespace TowerDefense.GameFlow.EndgamePopup
{
    public class EndgamePopupPresenter
    {
        private readonly SceneLoadingService _sceneLoadingService;
        private readonly SignalService _signalService;

        private EndgamePopupView _view;
        private EndgamePopupModel _model;

        public EndgamePopupPresenter()
        {
            _sceneLoadingService = ServiceLocator.GetService<SceneLoadingService>();
            _signalService = ServiceLocator.GetService<SignalService>();

            _signalService.GetSignal<ShowEndgamePopupSignal>().AddListener(OnShowPopup);
        }

        private void OnShowPopup(bool isWin)
        {
            InitializeView();
            InitializeModel();

            AddViewObservers();
            SetupModel(isWin);

            _view.Show();
        }

        private void InitializeView()
        {
            if (_view != null)
                return;

            if (!PopupsHandler.Instance.TryGetView<EndgamePopupView>(out var view))
                return;
            _view = view as EndgamePopupView;
        }

        private void InitializeModel()
        {
            _model ??= new EndgamePopupModel();
        }

        private void SetupModel(bool isWin)
        {
            _model.Message.Value = isWin ? "You Won!" : "You Lost!";
            _model.PanelColor.Value = isWin ? _view.WinPanelColor : _view.LosePanelColor;
        }

        private void AddViewObservers()
        {
            _model.Message.ValueChanged += _view.OnMessageChanged;
            _model.PanelColor.ValueChanged += _view.OnPanelColorChanged;

            _view.RestartButtonPressed += OnRestartButtonPressed;
            _view.MenuButtonPressed += OnMenuButtonPressed;
            _view.OnHidden += OnPopupHidden;
        }

        private void RemoveViewObservers()
        {
            _model.Message.ValueChanged -= _view.OnMessageChanged;
            _model.PanelColor.ValueChanged -= _view.OnPanelColorChanged;

            _view.RestartButtonPressed -= OnRestartButtonPressed;
            _view.MenuButtonPressed -= OnMenuButtonPressed;
            _view.OnHidden -= OnPopupHidden;
        }

        private void OnRestartButtonPressed()
        {
            _view.Hide();
            _signalService.GetSignal<GameRestartedSignal>().Send();
        }

        private void OnMenuButtonPressed()
        {
            _view.Hide();
            _sceneLoadingService.LoadStartMenuScene();
        }

        private void OnPopupHidden()
        {
            RemoveViewObservers();
        }
    }
}