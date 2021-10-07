using System.Linq;
using System.Reflection;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Nut.iOS.Views
{
    public static class NutIosViewLoader
    {
        public static TView FromNib<TView>(NSObject owner) where TView : class
        {
            var viewType = typeof(TView);
            var registerAttribute = viewType.GetCustomAttributes<RegisterAttribute>().FirstOrDefault();
            var name = registerAttribute != null ? registerAttribute.Name : viewType.Name;
            return FromNib(owner, name) as TView;
        }

        public static UIView FromNib(NSObject owner, string xibName)
        {
            return (UIView)Runtime.GetNSObject(NSBundle.MainBundle.LoadNib(xibName, owner, null).ValueAt(0));
        }
    }
}