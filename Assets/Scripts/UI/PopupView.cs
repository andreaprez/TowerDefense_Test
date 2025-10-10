using System;
using UnityEngine;

namespace TowerDefense.UI
{
    public abstract class PopupView : MonoBehaviour, IPopupView
    {
        public virtual void Show()
        {
            OnShown?.Invoke();
        }

        public virtual void Hide()
        {
            OnHidden?.Invoke();
        }

        public event Action OnShown;
        public event Action OnHidden;
    }
}