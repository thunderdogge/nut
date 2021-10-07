namespace Nut.Core.Application
{
    public interface INutApplicationLauncher
    {
        void Warmup();
        void Launch();
    }
}