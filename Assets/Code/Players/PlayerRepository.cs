using System.Collections.Generic;
using System.Linq;

namespace Code.Players
{
    /// <summary>
    /// Здесь хранятся все созданные игроки
    /// </summary>
    public sealed class PlayerRepository
    {
        private readonly Dictionary<int, PlayerController> _container = new();

        public PlayerController RegisterPlayer(Player player, PlayerView playerView)
        {
            var controller = new PlayerController(player, playerView);
            _container.Add(player.TeamId, controller);

            return controller;
        }
        
        public void Clear()
        {
            _container.Clear();
            _container.Clear();
        }

        public List<PlayerController> GetAll()
        {
            return _container.Values.ToList();
        }

        public PlayerController GetEnemy(int sourceTeamId)
        {
            return _container.FirstOrDefault(x => x.Value.Data.TeamId != sourceTeamId).Value;
        }
    }
}