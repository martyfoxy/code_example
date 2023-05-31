using Code.Players;
using Code.UI;
using Zenject;

namespace Code
{
    /// <summary>
    /// Занимается запуском нового боя
    /// </summary>
    public sealed class GameStarter : IInitializable
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly HpBarPresenter _hpBarPresenter;
        private readonly MainCanvasView _mainCanvasView;
        private readonly PlayerPanelsPresenter _playerPanelsPresenter;

        public GameStarter(
            PlayerSpawner playerSpawner,
            HpBarPresenter hpBarPresenter,
            MainCanvasView mainCanvasView,
            PlayerPanelsPresenter playerPanelsPresenter)
        {
            _playerSpawner = playerSpawner;
            _hpBarPresenter = hpBarPresenter;
            _mainCanvasView = mainCanvasView;
            _playerPanelsPresenter = playerPanelsPresenter;
        }

        public void Initialize()
        {
            _mainCanvasView.ReloadWithBuffsBtn.onClick.AddListener(StartGameWithBuffs);
            _mainCanvasView.DefaultReloadBtn.onClick.AddListener(StartDefaultGame);

            StartDefaultGame();
        }

        private void StartDefaultGame()
        {
            _playerSpawner.RespawnDefaultPlayers();
            ReInitGame();
        }

        private void StartGameWithBuffs()
        {
            _playerSpawner.RespawnBuffedPlayers();
            ReInitGame();
        }

        private void ReInitGame()
        {
            _hpBarPresenter.ReInitialize();
            _playerPanelsPresenter.ReInitialize();
        }
    }
}