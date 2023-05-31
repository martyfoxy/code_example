using System.Collections.Generic;
using Code.Players;
using Code.Settings;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code
{
    /// <summary>
    /// Точка входа
    /// </summary>
    public sealed class EntryInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraView cameraView;
        
        [SerializeField]
        private MainCanvasView mainCanvasView;

        [SerializeField]
        private PrefabsSettings prefabsSettings;

        [SerializeField]
        private List<Transform> spawnPoints;

        public override void InstallBindings()
        {
            Container.Bind<SettingsProvider>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<CameraController>()
                .AsSingle()
                .WithArguments(cameraView);
                
            Container.Bind<PlayerRepository>()
                .AsSingle();

            Container.Bind<PlayerSpawner>()
                .AsSingle()
                .WithArguments(spawnPoints);

            Container.BindInterfacesAndSelfTo<GameStarter>()
                .AsSingle();
            
            InstallUI();
        }

        private void InstallUI()
        {
            Container.Bind<PrefabsSettings>()
                .FromInstance(prefabsSettings)
                .AsSingle();
            
            Container.Bind<MainCanvasView>()
                .FromInstance(mainCanvasView)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerPanelsPresenter>()
                .AsSingle();

            Container.Bind<HpBarPresenter>()
                .AsSingle();
        }
    }
}