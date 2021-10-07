namespace Nut.Core.Application
{
    public interface INutApplicationEntry
    {
        INutApplicationLauncher CreateLauncher();
    }
}