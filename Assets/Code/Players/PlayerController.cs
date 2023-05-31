using System.Collections.Generic;
using Code.Data;
using Code.Parameters;
using Code.UI;

namespace Code.Players
{
    /// <summary>
    /// Агрегирует данные и визуал игрока, предоставляя интерфейс взаимодействия
    /// </summary>
    public class PlayerController
    {
        public Player Data { get; }
        public PlayerView View { get; }
        public HpBarView HpBar { get; }

        public readonly List<BuffModel> AppliedBuffs = new();
        
        public PlayerController(Player data, PlayerView view)
        {
            Data = data;
            View = view;
            HpBar = view.HpBarView;
            
            view.ResetAnimator(data.HealthParameters.Hp);
        }

        public void ApplyBuff(BuffModel buff)
        {
            AppliedBuffs.Add(buff);

            foreach (var buffStatModel in buff.stats)
            {
                var modifiedParameter = Data.GetParameter<float>(buffStatModel.statId);
                modifiedParameter.AddModifier(Modifier<float>.Add(buffStatModel.value));
            }
        }

        public void Attack(PlayerController target)
        {
            if (Data.HealthParameters.IsDead.Value || target.Data.HealthParameters.IsDead.Value)
                return;
            
            var dealtDamage = target.Data.BattleParameters.CalculateDamage(Data.BattleParameters.Damage.Value);
            var hpRestore = Data.BattleParameters.CalculateVampirism(dealtDamage);
            
            target.Data.HealthParameters.DecreaseHealth(dealtDamage);
            Data.HealthParameters.IncreaseHealth(hpRestore);

            View.PlayAttack();
        }
    }
}