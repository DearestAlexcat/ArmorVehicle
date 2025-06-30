using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public interface IExitableState
    {
        UniTask Exit();
    }
}
