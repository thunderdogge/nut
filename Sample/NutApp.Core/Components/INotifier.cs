using Nut.Core.Bindings.Commands;

namespace NutApp.Core.Components
{
    public interface INotifier
    {
        void NotifyLong(string message, string actionText = null, INutCommand command = null);
        void NotifyShort(string message, string actionText = null, INutCommand command = null);
    }
}