using System;
using System.Collections.Generic;

namespace Code.Parameters
{
    public sealed class Parameter<T> : IParameter<T>
    {
        public event Action<IParameter<T>> OnChanged;
        
        public int ID { get; }
        
        public T Value
        {
            get => _value;
            set
            {
                if (_baseValue.Equals(value))
                    return;

                _baseValue = value;
                RecalculateValue();

                OnChanged?.Invoke(this);
            }
        }

        public T PreviousValue { get; private set; }
        
        private readonly List<IModifier<T>> _modifiers;
        private readonly Func<T, T, T> _addFunc;
        private readonly Func<T, T, T> _multiplyFunc;
        
        private T _baseValue;
        private T _value;
        
        public Parameter(int id, T baseValue, Func<T,T,T> addFunc, Func<T,T,T> multiplyFunc)
        {
            ID = id;
            PreviousValue = default;
            
            _modifiers = new List<IModifier<T>>();
            _value = baseValue;
            _baseValue = baseValue;
            _addFunc = addFunc;
            _multiplyFunc = multiplyFunc;
        }

        public void AddModifier(IModifier<T> modifier)
        {
            _modifiers.Add(modifier);
            RecalculateValue();
        }
        
        public void RemoveModifier(IModifier<T> modifier)
        {
            _modifiers.Remove(modifier);
            RecalculateValue();
        }

        public void Reset()
        {
            _modifiers.Clear();
            RecalculateValue();
        }
        
        private void RecalculateValue()
        {
            PreviousValue = _value;
            _value = _baseValue;

            foreach (var modifier in _modifiers)
            {
                switch (modifier.OperationType)
                {
                    case ModifierOperationType.Additive:
                        _value = _addFunc(_value, modifier.Value);
                        break;
                    case ModifierOperationType.Multiplicative:
                        _value = _multiplyFunc(_value, modifier.Value);
                        break;
                }
            }

            OnChanged?.Invoke(this);
        }
    }
}