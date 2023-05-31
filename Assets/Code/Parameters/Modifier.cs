namespace Code.Parameters
{
    public class Modifier<T> : IModifier<T>
    {
        public ModifierOperationType OperationType { get; }
        public T Value { get; }
        
        public Modifier(ModifierOperationType operationType, T value)
        {
            OperationType = operationType;
            Value = value;
        }

        public static Modifier<T> Add(T value)
        {
            return new Modifier<T>(ModifierOperationType.Additive, value);
        }
        
        public static Modifier<T> Multiply(T value)
        {
            return new Modifier<T>(ModifierOperationType.Multiplicative, value);
        }
    }
}