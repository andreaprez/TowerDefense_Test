using TowerDefense.Service;
using TowerDefense.Signals;
using TowerDefense.UI;

namespace TowerDefense.GameFlow.EndgamePopup
{
    public class EndgamePopupPresenter
    {
        private EndgamePopupView _view;
        private EndgamePopupModel _model;

        public EndgamePopupPresenter()
        {
            ServiceLocator.GetService<SignalService>()
                .GetSignal<ShowEndgamePopupSignal>().AddListener(OnShowPopup);
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
        }

        private void AddViewObservers()
        {
            _model.Message.ValueChanged += _view.OnMessageChanged;

            _view.RestartButtonPressed += OnRestartButtonPressed;
            _view.MenuButtonPressed += OnMenuButtonPressed;
            _view.OnHidden += OnPopupHidden;
        }

        private void RemoveViewObservers()
        {
            _model.Message.ValueChanged -= _view.OnMessageChanged;

            _view.RestartButtonPressed -= OnRestartButtonPressed;
            _view.MenuButtonPressed -= OnMenuButtonPressed;
            _view.OnHidden -= OnPopupHidden;
        }

        private void OnMenuButtonPressed()
        {
            //TODO
        }

        private void OnRestartButtonPressed()
        {
            //TODO
        }

        private void OnPopupHidden()
        {
            RemoveViewObservers();
        }
    }
}