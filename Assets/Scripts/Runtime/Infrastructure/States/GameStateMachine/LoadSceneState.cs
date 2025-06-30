using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class LoadSceneState : IState<string>
    {
        readonly ISceneLoader sceneLoader;
        readonly ILogService logService;

        [Inject]
        public LoadSceneState(ISceneLoader sceneLoader, ILogService logService) 
        { 
            this.sceneLoader = sceneLoader;
            this.logService = logService;
        }

        public async UniTask Enter(string sceneName)
        {
            await sceneLoader.Load(sceneName);
            logService.Log($"Loaded: {sceneName} scene");
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}