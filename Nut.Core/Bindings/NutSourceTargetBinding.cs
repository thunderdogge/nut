using System.Collections;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutSourceTargetBinding : NutTargetBinding
    {
        private INutCollectionSource target;

        public NutSourceTargetBinding(INutCollectionSource target)
        {
            this.target = target;
        }

        public override void SetValue(object value)
        {
            var items = value as IEnumerable;
            if (items == null || target == null)
            {
                return;
            }

            target.Items = items;
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