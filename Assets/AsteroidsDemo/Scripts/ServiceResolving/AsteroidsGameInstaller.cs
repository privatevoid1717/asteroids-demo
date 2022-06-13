

//namespace AsteroidsDemo.Scripts.IOC
//{
    // public class AsteroidsGameInstaller : MonoInstaller
    // {
    //     [SerializeField] private Spawner.Spawner spawnerPrefab;
    //     [SerializeField] private VfxPlayer soundFxPlayerPrefab;
    //     [SerializeField] private PlayerInput playerInputPrefab;
    //
    //
    //     public override void InstallBindings()
    //     {
    //
    //         Container.Bind<PlayerInput>().FromComponentInNewPrefab(playerInputPrefab).AsSingle().NonLazy();
    //         Container.Bind<InputStatus>().AsSingle().NonLazy();
    //         Container.Bind<Spawner.Spawner>().FromComponentInNewPrefab(spawnerPrefab).AsSingle().NonLazy();
    //         Container.Bind<VfxPlayer>().FromComponentsInNewPrefab(soundFxPlayerPrefab).AsSingle().NonLazy();
    //         Container.Bind<ITiledCamera>().To<TiledCamera>().FromComponentsOn(Camera.main.gameObject).AsSingle();
    //     }
    // }
//}