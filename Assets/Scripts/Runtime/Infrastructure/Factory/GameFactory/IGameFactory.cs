using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArmorVehicle
{
    public interface IGameFactory
    {
        UniTask InitializeAsync();
        Projectile CreateProjectile(Transform parent = null);
        Enemy CreateEnemy(Transform parent = null);
        WinWindow CreateWinWindow(Transform parent = null);
        LoseWindow CreateLoseWindow(Transform parent = null);
        UniTask<T> InstantiatePrefabComponentAsync<T>(string assetKey, Transform parent = null) where T : Object;
    }
}