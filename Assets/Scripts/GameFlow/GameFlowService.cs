using TowerDefense.GameFlow.EndgamePopup;
using TowerDefense.Service;
using TowerDefense.Signals;

namespace TowerDefense.GameFlow
{
    public class GameFlowService : IService
    {
        public GameState GameState => _gameState;
        private GameState _gameState;

        private SignalService _signalService;

        public void Init()
        {
            _signalService = ServiceLocator.GetService<SignalService>();
            _signalService.GetSignal<GameStartedSignal>().AddListener(OnGameStarted);
            _signalService.GetSignal<GameEndedSignal>().AddListener(OnGameEnded);
            _signalService.GetSignal<GameRestartedSignal>().AddListener(OnGameRestarted);
        }

        private void OnGameStarted()
        {
            StartGame();
        }

        private void OnGameEnded(bool isWin)
        {
            EndGame(isWin);
        }

        private void OnGameRestarted()
        {
            StartGame();
        }

        private void StartGame()
        {
            _gameState = GameState.Gameplay;
        }

        private void EndGame(bool isWin)
        {
            _gameState = GameState.Endgame;
            _signalService.GetSignal<ShowEndgamePopupSignal>().Send(isWin);
        }
    }
}