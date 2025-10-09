using System;

namespace TowerDefense.Signals
{
    public abstract class Signal : ISignal
    {
        private Action _callback;
        
        public void AddListener(Action listener)
        {
            _callback += listener;
        }
        
        public void RemoveListener(Action listener)
        {
            _callback -= listener;
        }
        
        public void Send()
        {
            _callback?.Invoke();
        }
    }
    
    public abstract class Signal<T> : ISignal
    {
        private Action<T> _callback;
        
        public void AddListener(Action<T> listener)
        {
            _callback += listener;
        }
        
        public void RemoveListener(Action<T> listener)
        {
            _callback -= listener;
        }
        
        public void Send(T arg1)
        {
            _callback?.Invoke(arg1);
        }
    }
    
    public abstract class Signal<T1, T2> : ISignal
    {
        private Action<T1, T2> _callback;
        
        public void AddListener(Action<T1, T2> listener)
        {
            _callback += listener;
        }
        
        public void RemoveListener(Action<T1, T2> listener)
        {
            _callback -= listener;
        }
        
        public void Send(T1 arg1, T2 arg2)
        {
            _callback?.Invoke(arg1, arg2);
        }
    }
}