using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class InitGameplayState : IState
    {
        readonly GameplayStateMachine gameplayStateMachine;
        readonly IStaticDataService staticDataService;
        readonly ILogService logService;
        readonly IInteractorPoolContainer interactorPoolContainer;
        readonly ChunkPlacer chunkPlacer;
        readonly IGameFactory gameFactory;

        [Inject]
        public InitGameplayState(GameplayStateMachine gameplayStateMachine, IStaticDataService staticDataService, 
                                    ILogService logService, ChunkPlacer chunkPlacer, IInteractorPoolContainer interactorPoolContainer,
                                    IGameFactory gameFactory)
        {
            this.gameplayStateMachine = gameplayStateMachine;
            this.staticDataService = staticDataService;
            this.logService = logService;

            this.chunkPlacer = chunkPlacer;
            this.interactorPoolContainer = interactorPoolContainer;
            this.gameFactory = gameFactory;
        }

        public async UniTask Enter()
        {
            logService.Log("Entered InitGameplayState");

            CreatePoolContainers();
            chunkPlacer.Initialize(staticDataService);
            
            gameplayStateMachine.Enter<MenuState>().Forget();
        }

        void CreatePoolContainers()
        {
            CreateEnvironmentObjectPoolContainer();
            CreateProjectilePoolContainer(); 
            CreateEnemyPoolContainer();
        }

        void CreateEnvironmentObjectPoolContainer()
        {
            var factory = new EnvironmentObjectFactory(staticDataService.GetLevelConfig(0));
            interactorPoolContainer.CreatePool<EnvironmentObject, EnvironmentObjectFactory>(factory, 10, "Environment");
        }

        void CreateProjectilePoolContainer()
        {
            var factory = new ProjectileFactory(gameFactory);
            interactorPoolContainer.CreatePool<Projectile, ProjectileFactory>(factory, 10, "Projectile");
        }

        void CreateEnemyPoolContainer()
        {
            var factory = new EnemyFactory(gameFactory);
            interactorPoolContainer.CreatePool<Enemy, EnemyFactory>(factory, 10, "Enemy");
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}