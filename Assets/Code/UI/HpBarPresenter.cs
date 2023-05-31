using Code.Parameters;
using Code.Players;
using Code.Settings;
using Code.Utils;

namespace Code.UI
{
    /// <summary>
    /// Управляет отображением хп-баров игроков
    /// </summary>
    public class HpBarPresenter
    {
        private readonly PlayerRepository _playerRepository;
        private readonly Pool<DamageTakenView> _pool;
        
        public HpBarPresenter(
            PlayerRepository playerRepository,
            PrefabsSettings prefabsSettings)
        {
            _playerRepository = playerRepository;
            _pool = new Pool<DamageTakenView>(prefabsSettings.DamageBarPrefab, 3);
        }
        
        public void ReInitialize()
        {
            foreach (var controller in _playerRepository.GetAll())
            {
                controller.HpBar.SetValue(controller.Data.HealthParameters.Hp, controller.Data.HealthParameters.MaxHp);
                
                controller.Data.HealthParameters.Hp.OnChanged += (hpParameter) =>
                {
                    OnHpChanged(controller, hpParameter);
                };
            }
        }

        private void OnHpChanged(PlayerController controller, IParameter<float> hpParameter)
        {
            controller.HpBar.SetValue(hpParameter, controller.Data.HealthParameters.MaxHp);
            
            var takenDamage = hpParameter.PreviousValue - hpParameter.Value;
            if (takenDamage <= 0f)
                return;
            
            var damageView = _pool.Spawn(controller.HpBar.transform);
            damageView.ShowDamage(takenDamage, () => _pool.Despawn(damageView));
        }
    }
}