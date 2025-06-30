using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        LevelConfig GetLevelConfig(int levelIndex);
        CarConfig CarConfig { get; }
        ProjectileConfig ProjectileConfig { get; }
        EnemyConfig EnemyConfig { get; }
    }
}