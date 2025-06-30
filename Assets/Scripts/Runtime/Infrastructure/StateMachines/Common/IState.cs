using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }

    public interface IState<TParam1> : IExitableState
    {
        UniTask Enter(TParam1 param1);
    }
}