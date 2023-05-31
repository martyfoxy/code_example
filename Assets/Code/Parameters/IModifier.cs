namespace Code.Parameters
{
    /// <summary>
    /// Описывает модификацию какого-либо параметра
    /// </summary>
    public interface IModifier<T>
    {
        ModifierOperationType OperationType { get; }
        T Value { get; }
    }
}