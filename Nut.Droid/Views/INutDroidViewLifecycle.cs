using System;
using Android.OS;
using Nut.Core.Views;

namespace Nut.Droid.Views
{
    public interface INutDroidViewLifecycle : INutViewLifecycle
    {
        void OnAfterCreate(INutView view, Type modeType, Bundle bundle);
        void OnBeforeCreate(INutView view, Type modeType, Bundle bundle);
        void OnBeforeSaveInstanceState(INutView view, Bundle bundle);
        void OnAfterSaveInstanceState(INutView view, Bundle bundle);
    }
}