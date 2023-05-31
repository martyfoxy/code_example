using System.Collections.Generic;
using Code.Players;

namespace Code.Parameters
{
    public class BattleParameters : IBattleParameters
    {
        public IParameter<float> Armor { get; }
        public IParameter<float> Damage { get; }
        public IParameter<float> Vampirism { get; }

        public BattleParameters(PlayerSpawnData spawnData)
        {
            Armor = new Parameter<float>(ParametersConst.Armor, spawnData.Armor, ParametersOperations.Add, ParametersOperations.Multiply);
            Damage = new Parameter<float>(ParametersConst.Damage, spawnData.Damage, ParametersOperations.Add, ParametersOperations.Multiply);
            Vampirism = new Parameter<float>(ParametersConst.Vampirism, spawnData.Vampirism, ParametersOperations.Add, ParametersOperations.Multiply);
        }

        public float CalculateDamage(float damage)
        {
            var resultDamage = damage * ((100f - Armor.Value) / 100f);
            
            if (resultDamage < 0)
                resultDamage = 0;
            
            return resultDamage;
        }
        
        public float CalculateVampirism(float damage)
        {
            var resultVampirism = Vampirism.Value / 100f * damage;
            
            return resultVampirism;
        }

        public Dictionary<int, object> GetParameters()
        {
            return new Dictionary<int, object>
            {
                {
                    Armor.ID,
                    Armor
                },
                {
                    Damage.ID,
                    Damage
                },
                {
                    Vampirism.ID,
                    Vampirism
                }
            };
        }
    }
}