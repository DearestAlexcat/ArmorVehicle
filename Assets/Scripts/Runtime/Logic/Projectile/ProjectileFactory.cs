
namespace ArmorVehicle
{
    public class ProjectileFactory : IObjectFactory<Projectile>
    {
        IGameFactory gameFactory;

        public ProjectileFactory(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public Projectile Create()
        {
            return gameFactory.CreateProjectile();
        }
    }
}