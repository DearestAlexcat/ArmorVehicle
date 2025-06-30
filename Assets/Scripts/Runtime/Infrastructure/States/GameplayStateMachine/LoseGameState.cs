using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class LoseGameState : IState
    {
        IGameFactory gameFactory;

        [Inject]
        void Construct(IGameFactory gameFactory)
        {
            this.gameFactory = gameFactory;
        }

        public UniTask Enter()
        {
            gameFactory.CreateLoseWindow();
            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}