using Nut.Core.Navigation;

namespace Nut.Core.Application
{
    public abstract class NutApplicationStart : NutNavigatableObject, INutApplicationStart
    {
        public abstract void Start();
    }
}