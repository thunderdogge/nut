namespace NutApp.Core.Components
{
    public interface IToaster
    {
        void ShowLong(string message);
        void ShowShort(string message);
    }
}