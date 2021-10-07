using Nut.Core.Navigation;
using Nut.Ioc;

namespace Nut.Core.Models
{
    [NutIocIgnore]
    public abstract class NutViewModel : NutNavigatableObject, INutViewModel
    {
        public object State { get; set; }

        public virtual void Start()
        {
        }

        public virtual void Resume()
        {
        }

        public virtual void Pause()
        {
        }

        public virtual void Stop()
        {
        }

        public virtual void Finish()
        {
        }
    }
}