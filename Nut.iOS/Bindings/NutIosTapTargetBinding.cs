using System;
using Foundation;
using Nut.Core.Bindings;
using UIKit;

namespace Nut.iOS.Bindings
{
    public class NutIosTapTargetBinding : NutTargetBinding
    {
        private INutTargetBinding targetBinding;

        public NutIosTapTargetBinding(NSObject target)
        {
            targetBinding = CreateTargetBinding(target);
        }

        public override void SetValue(object value)
        {
            targetBinding.SetValue(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (targetBinding != null)
                {
                    targetBinding.Dispose();
                    targetBinding = null;
                }
            }

            base.Dispose(disposing);
        }

        private static INutTargetBinding CreateTargetBinding(NSObject target)
        {
            var uiView = target as UIView;
            if (uiView != null)
            {
                return new NutIosTapViewTargetBinding(uiView);
            }

            var uiBarButton = target as UIBarButtonItem;
            if (uiBarButton != null)
            {
                return new NutIosTapBarTargetBinding(uiBarButton);
            }

            throw new ArgumentException("`" + target.GetType() + "` is not supported for tap tap binding");
        }
    }
}