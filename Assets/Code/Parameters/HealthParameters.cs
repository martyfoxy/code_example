using System.Collections.Generic;

namespace Code.Parameters
{
    public class HealthParameters : IHealthParameters
    {
        public IParameter<float> Hp { get; }
        public IParameter<float> MaxHp { get; }
        public IParameter<bool> IsDead { get; }

        public HealthParameters(float maxHp)
        {
            Hp = new Parameter<float>(ParametersConst.Hp, maxHp, ParametersOperations.Add, ParametersOperations.Multiply);
            MaxHp = new Parameter<float>(ParametersConst.MaxHp, maxHp, ParametersOperations.Add, ParametersOperations.Multiply);
            IsDead = new Parameter<bool>(ParametersConst.IsDead, false, ParametersOperations.Add, ParametersOperations.Multiply);
        }
        
        public void IncreaseHealth(float amount)
        {
            var newHp = Hp.Value + amount;
            if (newHp > MaxHp.Value)
                Hp.Value = MaxHp.Value;
            else
                Hp.Value += amount;
        }
        
        public void DecreaseHealth(float amount)
        {
            var newHp = Hp.Value - amount;
            if (newHp < 0f)
            {
                Hp.Value = 0f;
                IsDead.Value = true;
            }
            else
                Hp.Value -= amount;
        }
        
        public void Restore()
        {
            if (!IsDead.Value)
                return;
            
            Hp.Value = MaxHp.Value;
            IsDead.Value = false;
        }

        public Dictionary<int, object> GetParameters()
        {
            return new Dictionary<int, object>
            {
                {
                    Hp.ID,
                    Hp
                },
                {
                    MaxHp.ID,
                    MaxHp
                },
                {
                    IsDead.ID,
                    IsDead
                }
            };
        }
    }
}