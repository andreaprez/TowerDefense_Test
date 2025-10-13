using System;
using UnityEngine;

namespace TowerDefense.UI
{
    public abstract class PopupView : MonoBehaviour, IPopupView
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnShown?.Invoke();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHidden?.Invoke();
        }

        public event Action OnShown;
        public event Action OnHidden;
    }
}