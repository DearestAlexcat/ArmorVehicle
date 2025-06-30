using System;

namespace ArmorVehicle
{
    public enum SceneName
    {
        Level = 0,
    }

    public enum WindowType
    {
        WinWindow = 0,
        LoseWindow = 1
    }

    public enum EnemyState
    {
        Idle = 0,
        Pursuit = 1,
        Wander = 3,
        Stop = 4,
    }

    [Flags]
    public enum EnemyType
    {
        None = 0,
        RedStickman = 1 << 0,
        BlueStickman = 1 << 1,
    }
}