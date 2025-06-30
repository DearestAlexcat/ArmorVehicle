using Cysharp.Threading.Tasks;
using Zenject;

namespace ArmorVehicle
{
    public class StateMachine : IStateMachine
    {
        IStatesFactory statesFactory;

        [Inject]
        void Construct(IStatesFactory statesFactory)
        {
            this.statesFactory = statesFactory;
        }

        public void SetStatesFactory(IStatesFactory factory)
        {
            this.statesFactory = factory;
        }

        IExitableState currentState;
        public void UpdateState()
        {
            if(currentState != null && currentState is IUpdatebleState updateble)
            {
                updateble.Update();
            }
        }

        TState GetState<TState>() where TState : class, IExitableState
        {
            return statesFactory.GetState<TState>();
        }

        async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            TState newState = GetState<TState>();
            
            if(currentState != null)
                await currentState.Exit();

            currentState = newState;

            return newState;
        }


        public async UniTask Enter<TState>() where TState : class, IState
        {
            IState state = await ChangeState<TState>();
            if(state != null)
                await state.Enter();
        }

        public async UniTask Enter<TState, Param1>(Param1 param1) where TState : class, IState<Param1>
        {
            IState<Param1> state = await ChangeState<TState>();
            if (state != null)
                await state.Enter(param1);
        }
    }
}