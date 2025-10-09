using TowerDefense.Service;
using UnityEngine;

namespace TowerDefense.GameFlow
{
    public class GameFlowService : IService
    {
        public GameState GameState => _gameState;
        private GameState _gameState;

        public void Init() { }
        public void Dispose() { }

        public void StartGame()
        {
            _gameState = GameState.Gameplay;
        }

        public void EndGame(bool isWin)
        {
            _gameState = GameState.Endgame;
            if (isWin)
                Debug.Log("Win!"); //TODO: Show win
            else
                Debug.Log("Lose!"); //TODO: Show lose
        }
    }
}