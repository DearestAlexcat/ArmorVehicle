using System;

namespace ArmorVehicle
{
    public interface IInputService
    {
        event Action<float> OnSwipe;
        event Action OnButtonDown;
        event Action OnButtonUp;
    }
}