using System;

namespace Utils
{
    public class Observable<T>
    {
        private T _value;

        public Observable(T value)
        {
            _value = value;
        }

        public event Action ValueChanged;
        public T Value {
            get => _value;
            set
            {
                if (Equals(value, _value))
                    return;
                _value = value;
                ValueChanged?.Invoke();
            }
        }
    }
}