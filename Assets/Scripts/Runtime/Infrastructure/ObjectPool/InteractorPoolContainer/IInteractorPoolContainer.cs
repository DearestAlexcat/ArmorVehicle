using UnityEngine;

namespace ArmorVehicle
{
    public interface IInteractorPoolContainer
    {
        bool TryGet<T>(out T interactor) where T : IPoolObject<T>;
        IPoolObject<T> Get<T>() where T : Component;
        void CreatePool<T, T1>(T1 factory, int initialCount, string poolName) where T : Component where T1 : IObjectFactory<T>;
        void CleanUp();
    }
}