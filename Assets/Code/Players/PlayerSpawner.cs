using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Parameters;
using Code.Settings;
using Code.Utils;
using UnityEngine;

namespace Code.Players
{
    /// <summary>
    /// Занимается спавном игроков
    /// </summary>
    public sealed class PlayerSpawner
    {
        private readonly SettingsProvider _settingsProvider;
        private readonly PlayerRepository _playerRepository;
        private readonly List<Transform> _spawnPoints;
        private readonly Pool<PlayerView> _pool;

        public PlayerSpawner(
            SettingsProvider settingsProvider,
            PrefabsSettings prefabsSettings,
            PlayerRepository playerRepository,
            List<Transform> spawnPoints)
        {
            _settingsProvider = settingsProvider;
            _playerRepository = playerRepository;
            _spawnPoints = spawnPoints;
            _pool = new Pool<PlayerView>(prefabsSettings.PlayerPrefab, _settingsProvider.Root.settings.playersCount);

            if (spawnPoints.Count < _settingsProvider.Root.settings.playersCount)
                Debug.LogError("Not enough spawn points");
        }
        
        public void RespawnDefaultPlayers()
        {
            DespawnPlayers();
            
            for (var i = 0; i < _settingsProvider.Root.settings.playersCount; i++)
            {
                var spawnData = ConvertToSpawnData(i, _settingsProvider.Root.stats);
                
                var playerData = CreatePlayerData(spawnData);
                var playerView = _pool.Spawn(spawnData.SpawnPoint);
                
                _playerRepository.RegisterPlayer(playerData, playerView);
            }
        }
        
        public void RespawnBuffedPlayers()
        {
            DespawnPlayers();
            
            for (var i = 0; i < _settingsProvider.Root.settings.playersCount; i++)
            {
                var spawnData = ConvertToSpawnData(i, _settingsProvider.Root.stats);
                
                var playerData = CreatePlayerData(spawnData);
                var playerView = _pool.Spawn(spawnData.SpawnPoint);
                
                var controller = _playerRepository.RegisterPlayer(playerData, playerView);
                
                //Применение бафов
                var buffs = GetRandomBuffs();
                foreach (var buffModel in buffs)
                    controller.ApplyBuff(buffModel);
            }
        }
        
        private Player CreatePlayerData(PlayerSpawnData spawnData)
        {
            var healthParameters = new HealthParameters(spawnData.Hp);
            var battleParameters = new BattleParameters(spawnData);

            return new Player(spawnData.TeamId, healthParameters, battleParameters);
        }

        private void DespawnPlayers()
        {
            foreach (var controller in _playerRepository.GetAll())
                _pool.Despawn(controller.View);
            
            _playerRepository.Clear();
        }

        private PlayerSpawnData ConvertToSpawnData(int teamId, StatModel[] statModels)
        {
            //В настроечном файле стартовые параметры имеют id
            //Здесь мы указываем какой id какому параметру соответствует 
            var result = new PlayerSpawnData
            {
                TeamId = teamId,
                Hp = statModels.FirstOrDefault(x => x.id == ParametersConst.Hp)?.value ?? 0,
                Armor = statModels.FirstOrDefault(x => x.id == ParametersConst.Armor)?.value ?? 0,
                Damage = statModels.FirstOrDefault(x => x.id == ParametersConst.Damage)?.value ?? 0,
                Vampirism = statModels.FirstOrDefault(x => x.id == ParametersConst.Vampirism)?.value ?? 0,
                SpawnPoint = _spawnPoints[teamId]
            };
            
            return result;
        }
        
        private List<BuffModel> GetRandomBuffs()
        {
            var buffList = new List<BuffModel>();
            var buffCount = Random.Range(_settingsProvider.Root.settings.buffCountMin, _settingsProvider.Root.settings.buffCountMax + 1);

            for (var i = 0; i < buffCount; i++)
            {
                var randomIndex = Random.Range(0, _settingsProvider.Root.buffs.Length - 1);
                var randomBuff = _settingsProvider.Root.buffs[randomIndex];

                if (_settingsProvider.Root.settings.allowDuplicateBuffs || !buffList.Contains(randomBuff))
                    buffList.Add(randomBuff);
            }

            return buffList;
        }
    }
}