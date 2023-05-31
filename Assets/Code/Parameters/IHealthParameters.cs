namespace Code.Parameters
{
    /// <summary>
    /// Описывает параметры системы здоровья и методы для их изменения
    /// </summary>
    public interface IHealthParameters : IPlayerParametersContainer
    {
        IParameter<float> Hp { get; }
        IParameter<float> MaxHp { get; }
        IParameter<bool> IsDead { get; }

        void IncreaseHealth(float amount);
        void DecreaseHealth(float amount);
        void Restore();
    }
}