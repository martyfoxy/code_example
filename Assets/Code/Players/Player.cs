using System.Collections.Generic;
using Code.Parameters;
using UnityEngine;

namespace Code.Players
{
    /// <summary>
    /// Представляет из себя набор данных, которые описывают текущее состояние игрока
    /// </summary>
    public sealed class Player
    {
        public int TeamId { get; }
        public IHealthParameters HealthParameters { get; }
        public IBattleParameters BattleParameters { get; }

        private readonly Dictionary<int, object> _allParameters = new();

        public Player(
            int teamId,
            IHealthParameters healthParameters,
            IBattleParameters battleParameters)
        {
            TeamId = teamId;
            HealthParameters = healthParameters;
            BattleParameters = battleParameters;

            AddParametersToDictionary(HealthParameters.GetParameters());
            AddParametersToDictionary(BattleParameters.GetParameters());
        }

        public IParameter<T> GetParameter<T>(int id)
        {
            if(!_allParameters.TryGetValue(id, out var parameter))
                Debug.LogError($"Couldn't find parameter with id {id}");

            return parameter as IParameter<T>;
        }

        private void AddParametersToDictionary<T>(Dictionary<int, T> parameters)
        {
            foreach (var (id, parameter) in parameters)
                _allParameters.Add(id, parameter);
        }
    }
}