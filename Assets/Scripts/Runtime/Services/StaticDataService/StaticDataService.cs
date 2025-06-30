using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ArmorVehicle
{
    public class StaticDataService : IStaticDataService
    {
        readonly ILogService logService;
        readonly IAssetProvider assetProvider;

        public CarConfig CarConfig { get; private set; }
        public EnemyConfig EnemyConfig { get; private set; }
        public ProjectileConfig ProjectileConfig { get; private set; }

        Dictionary<int, LevelConfig> levelConfig;

        public StaticDataService(ILogService logService, IAssetProvider assetProvider)
        {
            this.logService = logService;
            this.assetProvider = assetProvider;
        }

        public LevelConfig GetLevelConfig(int levelIndex)
        {
            return levelConfig[levelIndex];
        }

        public async UniTask InitializeAsync()
        {
            await UniTask.WhenAll(LoadLevelConfigs(), LoadCarConfig(), LoadEnemyConfig(), LoadProjectileConfig());
            logService.Log("Shop static data loaded");
        }

        async UniTask LoadLevelConfigs()
        {
            var configs = await GetConfigs<LevelConfig>(AssetPath.LevelLabel);
            levelConfig = configs.ToDictionary(config => config.levelIndex, config => config);
        }

        async UniTask LoadCarConfig()
        {
            var configs = await GetConfigs<CarConfig>(AssetPath.LevelLabel);
            if(configs.Length > 0)
                CarConfig = configs[0];
            else logService.LogError("There are no player config founded!");
        }

        async UniTask LoadEnemyConfig()
        {
            var configs = await GetConfigs<EnemyConfig>(AssetPath.LevelLabel);
            if (configs.Length > 0)
                EnemyConfig = configs[0];
            else logService.LogError("There are no player config founded!");
        }

        async UniTask LoadProjectileConfig()
        {
            var configs = await GetConfigs<ProjectileConfig>(AssetPath.LevelLabel);
            if (configs.Length > 0)
                ProjectileConfig = configs[0];
            else logService.LogError("There are no player config founded!");
        }

        async UniTask<TConfig[]> GetConfigs<TConfig>(string label) where TConfig : class
        {
            var keys = await assetProvider.GetAssetPrimaryKeysByLabel<TConfig>(label);
            return await assetProvider.LoadAll<TConfig>(keys);
        }
    }
}