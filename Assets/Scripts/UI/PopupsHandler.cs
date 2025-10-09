using System;
using System.Collections.Generic;
using TowerDefense.GameFlow.EndgamePopup;
using UnityEngine;

namespace TowerDefense.UI
{
    public class PopupsHandler : MonoBehaviour
    {
        [SerializeField] private Transform _popupsRoot;
        [SerializeField] private List<PopupView> _popupPrefabs;

        public static PopupsHandler Instance => _instance;
        private static PopupsHandler _instance;

        private List<IPopupView> _instantiatedPopupViews;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Destroy(this);
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _instantiatedPopupViews = new List<IPopupView>();
            SetupPopupPresenters();
        }

        private void SetupPopupPresenters()
        {
            new EndgamePopupPresenter();
        }

        public bool TryGetView<T>(out IPopupView popupView)
        {
            var viewType = typeof(T);
            
            if (TryGetInstantiatedView(viewType, out popupView))
            {
                return true;
            }

            if (TryInstantiateView(viewType, out popupView))
            {
                _instantiatedPopupViews.Add(popupView);
                return true;
            }

            popupView = default;
            return false;
        }

        private bool TryGetInstantiatedView(Type viewType, out IPopupView popupView)
        {
            foreach (var view in _instantiatedPopupViews)
            {
                if (view.GetType() == viewType)
                {
                    popupView = view;
                    return true;
                }
            }

            popupView = default;
            return false;
        }

        private bool TryInstantiateView(Type viewType, out IPopupView popupView)
        {
            popupView = _popupPrefabs.Find(popup => popup.GetType() == viewType);
            if (popupView != null)
            {
                var popupViewInstance = Instantiate((popupView as Component).gameObject, _popupsRoot);
                popupViewInstance.name = (popupView as Component).gameObject.name.Replace("View", "");
                return true;
            }

            popupView = default;
            return false;
        }
    }
}