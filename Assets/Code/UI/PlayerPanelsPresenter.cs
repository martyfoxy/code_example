using System.Collections.Generic;
using System.Globalization;
using Code.Data;
using Code.Parameters;
using Code.Players;
using Code.Settings;
using Code.Utils;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    /// <summary>
    /// Управляет отображением панелей управления игроков
    /// </summary>
    public sealed class PlayerPanelsPresenter : IInitializable
    {
        private readonly MainCanvasView _mainCanvasView;
        private readonly PlayerRepository _playerRepository;
        private readonly SettingsProvider _settingsProvider;
        private readonly Pool<StatView> _pool;

        private readonly List<StatView> _spawnedStats = new();
        private readonly List<StatView> _spawnedBuffs = new();
        private readonly Dictionary<int, Sprite> _statsSpriteCache = new();
        private readonly Dictionary<int, Sprite> _buffsSpriteCache = new();

        public PlayerPanelsPresenter(
            MainCanvasView mainCanvasView, 
            PlayerRepository playerRepository,
            PrefabsSettings prefabsSettings,
            SettingsProvider settingsProvider)
        {
            _mainCanvasView = mainCanvasView;
            _playerRepository = playerRepository;
            _settingsProvider = settingsProvider;
            _pool = new Pool<StatView>(prefabsSettings.StatPrefab, 8);
        }

        public void Initialize()
        {
            if (_mainCanvasView.ControlPanels.Count < _settingsProvider.Root.settings.playersCount)
            {
                Debug.LogError("Not enough control panels");
                return;
            }
        }

        public void ReInitialize()
        {
            ClearPanel();
            
            foreach (var controller in _playerRepository.GetAll())
            {
                var panelView = _mainCanvasView.ControlPanels[controller.Data.TeamId];
                var enemyController = _playerRepository.GetEnemy(controller.Data.TeamId);
                
                //Обработка атаки
                panelView.SetAttackHandler(() =>
                {
                    controller.Attack(enemyController);
                    enemyController.View.SetHpParameter(enemyController.Data.HealthParameters.Hp);
                });
                
                //Спавним статы
                foreach (var stat in _settingsProvider.Root.stats)
                {
                    var statView = AddStatToPanel(controller.Data.TeamId, stat, panelView.StatsPanel);
                    var parameter = controller.Data.GetParameter<float>(stat.id);
                    statView.SetLabel(parameter.Value.ToString(CultureInfo.InvariantCulture));

                    //Динамически изменяется только HP
                    if (parameter.ID == ParametersConst.Hp)
                    {
                        controller.Data.HealthParameters.Hp.OnChanged += hpParameter =>
                        {
                            statView.SetLabel(hpParameter.Value.ToString(CultureInfo.InvariantCulture));
                        };
                    }
                }
                
                foreach (var playerBuff in controller.AppliedBuffs)
                    AddBuffToPanel(controller.Data.TeamId, playerBuff, panelView.StatsPanel);
            }
        }
        
        private void ClearPanel()
        {
            foreach (var view in _spawnedBuffs)
                _pool.Despawn(view);
                
            foreach (var view in _spawnedStats)
                _pool.Despawn(view);
            
            _spawnedStats.Clear();
            _spawnedBuffs.Clear();
        }
        
        private StatView AddStatToPanel(int teamId, StatModel statModel, Transform panel)
        {
            if (!_statsSpriteCache.TryGetValue(statModel.id, out var sprite))
            {
                sprite = Resources.Load<Sprite>("Icons/" + statModel.icon);
                _statsSpriteCache.Add(statModel.id, sprite);
            }
            
            var statView = SpawnStatView(panel, sprite);
            _spawnedStats.Add(statView);

            return statView;
        }

        private void AddBuffToPanel(int teamId, BuffModel buffModel, Transform panel)
        {
            if (!_buffsSpriteCache.TryGetValue(buffModel.id, out var sprite))
            {
                sprite = Resources.Load<Sprite>("Icons/" + buffModel.icon);
                _buffsSpriteCache.Add(buffModel.id, sprite);
            }

            var buffView = SpawnStatView(panel, sprite, buffModel.title);
            _spawnedBuffs.Add(buffView);
        }
        
        private StatView SpawnStatView(Transform panel, Sprite icon, string text = default)
        {
            var statView = _pool.Spawn(panel);
            statView.SetIcon(icon);
            statView.SetLabel(text);

            return statView;
        }
    }
}