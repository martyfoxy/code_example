namespace Code.Parameters
{
    /// <summary>
    /// Описывает параметры относящися к боевой системе
    /// </summary>
    public interface IBattleParameters : IPlayerParametersContainer
    {
        IParameter<float> Armor { get; }
        IParameter<float> Damage { get; }
        IParameter<float> Vampirism { get; }

        float CalculateDamage(float damage);
        float CalculateVampirism(float damage);
    }
}