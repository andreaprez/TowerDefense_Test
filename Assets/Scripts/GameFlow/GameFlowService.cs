using TowerDefense.Service;
using TowerDefense.Signals;
using UnityEngine;

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
        }

        public void Dispose()
        {
            _signalService.GetSignal<GameStartedSignal>().RemoveListener(OnGameStarted);
            _signalService.GetSignal<GameEndedSignal>().RemoveListener(OnGameEnded);
            _signalService = null;
        }

        private void OnGameStarted()
        {
            StartGame();
        }

        private void OnGameEnded(bool isWin)
        {
            EndGame(isWin);
        }

        private void StartGame()
        {
            _gameState = GameState.Gameplay;
        }

        private void EndGame(bool isWin)
        {
            _gameState = GameState.Endgame;
            if (isWin)
                Debug.Log("Win!"); //TODO: Show win
            else
                Debug.Log("Lose!"); //TODO: Show lose
        }
    }
}