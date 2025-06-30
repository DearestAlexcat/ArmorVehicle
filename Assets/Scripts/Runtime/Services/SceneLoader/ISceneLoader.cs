
using Cysharp.Threading.Tasks;

namespace ArmorVehicle
{
    public interface ISceneLoader
    {
        UniTask Load(string nextScene);
    }
}