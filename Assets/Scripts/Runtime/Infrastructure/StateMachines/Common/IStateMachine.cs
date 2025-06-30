
using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public interface IStateMachine
    {
        UniTask Enter<TState>() where TState : class, IState;
        UniTask Enter<TState, Param1>(Param1 param) where TState : class, IState<Param1>;
    }
}