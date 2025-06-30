using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArmorVehicle
{
    public class GameFactory : IGameFactory
    {
        private IInstantiator instantiator;
        private IAssetProvider assetProvider;

        GameObject enemyPrefab;
        GameObject projectilePrefab;
        GameObject winWindowPrefab;
        GameObject loseWindowPrefab;

        public GameFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            this.instantiator = instantiator;
            this.assetProvider = assetProvider;
        }

        public async UniTask InitializeAsync()
        {
            enemyPrefab = await assetProvider.Load<GameObject>(AssetPath.Enemy);
            projectilePrefab = await assetProvider.Load<GameObject>(AssetPath.Projectile);
            winWindowPrefab = await assetProvider.Load<GameObject>(AssetPath.WinWindow);
            loseWindowPrefab = await assetProvider.Load<GameObject>(AssetPath.LoseWindow);
        }

        public WinWindow CreateWinWindow(Transform parent = null)
        {
            GameObject newObject = instantiator.InstantiatePrefab(winWindowPrefab, parent);
            return newObject.GetComponent<WinWindow>();
        }

        public LoseWindow CreateLoseWindow(Transform parent = null)
        {
            GameObject newObject = instantiator.InstantiatePrefab(loseWindowPrefab, parent);
            return newObject.GetComponent<LoseWindow>();
        }

        public Projectile CreateProjectile(Transform parent = null)
        {
            GameObject newObject = instantiator.InstantiatePrefab(projectilePrefab, parent);
            return newObject.GetComponent<Projectile>();
        }

        public Enemy CreateEnemy(Transform parent = null)
        {
            GameObject newObject = instantiator.InstantiatePrefab(enemyPrefab, parent);
            return newObject.GetComponent<Enemy>();
        }

        public async UniTask<T> InstantiatePrefabComponentAsync<T>(string assetKey, Transform parent = null) where T : Object
        {
            GameObject prefab = await assetProvider.Load<GameObject>(assetKey);
            GameObject newObject = instantiator.InstantiatePrefab(prefab, parent);
            return newObject.GetComponent<T>();
        }
    }
}