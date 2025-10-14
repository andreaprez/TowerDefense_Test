using System;
using System.Collections.Generic;
using TowerDefense.Service;

namespace TowerDefense.Signals
{
    public class SignalService : IService
    {
        private Dictionary<Type, ISignal> _signals;

        public void Init()
        {
            _signals = new Dictionary<Type, ISignal>();
        }

        public T GetSignal<T>() where T : ISignal, new()
        {
            var signalType = typeof(T);

            if (_signals.TryGetValue(signalType, out var signal)) 
                return (T)signal;

            return RegisterSignal<T>(signalType);
        }

        private T RegisterSignal<T>(Type signalType) where T : ISignal
        {
            var signal = (ISignal)Activator.CreateInstance(signalType);
            _signals.Add(signalType, signal);
            return (T)signal;
        }
    }
}