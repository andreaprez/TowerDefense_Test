using TowerDefense.Service;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scene
{
    public class SceneLoadingService : IService
    {
        private const int StartMenuSceneIndex = 0;
        private const int GameplaySceneIndex = 1;

        public void Init() { }

        public void LoadStartMenuScene()
        {
            SceneManager.LoadScene(StartMenuSceneIndex);
        }

        public void LoadGameplayScene()
        {
            SceneManager.LoadScene(GameplaySceneIndex);
        }
    }
}