
namespace ArmorVehicle
{
    public interface IHudView
    {
        void Display(bool value);
        void UpdateHUD(MovementTracker tracker);
    }
}