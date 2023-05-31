using System;

namespace Code.Parameters
{
    /// <summary>
    /// Описывает какой-либо параметр. Имеет базовое значение и может быть модифицирован
    /// </summary>
    public interface IParameter<T>
    {
        event Action<IParameter<T>> OnChanged;
        
        int ID { get; }
        T Value { get; set; }
        T PreviousValue { get; }

        void AddModifier(IModifier<T> modifier);
        void RemoveModifier(IModifier<T> modifier);
        void Reset();
    }
}