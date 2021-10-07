using Nut.Core.Bindings.Commands;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutSourceSelectTargetBinding : NutTargetCommandBinding
    {
        private INutCollectionSource target;

        public NutSourceSelectTargetBinding(INutCollectionSource target)
        {
            this.target = target;
        }

        protected override void SetCommandValue(INutCommand command)
        {
            target.ItemSelectCommand = command;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                target?.Dispose();
                target = null;
            }

            base.Dispose(disposing);
        }
    }
}