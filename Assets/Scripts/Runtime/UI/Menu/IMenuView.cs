using System;

namespace ArmorVehicle
{
    public interface IMenuView
    {
        event Action OnStartGame;
        void Display(bool value);
    }
}