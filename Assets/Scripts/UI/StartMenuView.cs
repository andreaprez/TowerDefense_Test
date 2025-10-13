using TowerDefense.Scene;
using TowerDefense.Service;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class StartMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private SceneLoadingService _sceneLoadingService;

        private void Start()
        {
            _sceneLoadingService = ServiceLocator.GetService<SceneLoadingService>();

            _playButton.onClick.AddListener(OnPlayButtonPressed);
            _quitButton.onClick.AddListener(OnQuitButtonPressed);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonPressed);
            _quitButton.onClick.RemoveListener(OnQuitButtonPressed);
        }

        private void OnPlayButtonPressed()
        {
            _sceneLoadingService.LoadGameplayScene();
        }

        private void OnQuitButtonPressed()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
