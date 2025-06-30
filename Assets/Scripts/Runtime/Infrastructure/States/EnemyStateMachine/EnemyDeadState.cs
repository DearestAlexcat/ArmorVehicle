using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public class EnemyDeadState : IState
    {
        readonly EnemyStateMachine stateMachine;

        public EnemyDeadState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public UniTask Enter()
        {
            return default;
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}
