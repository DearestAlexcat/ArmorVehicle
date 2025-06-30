
namespace ArmorVehicle
{
    public class EnemyFactory : IObjectFactory<Enemy>
    {
        IGameFactory gameFactory;

        public EnemyFactory(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public Enemy Create()
        {
            return gameFactory.CreateEnemy();
        }
    }
}