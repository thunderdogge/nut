using System;

namespace Nut.Core.Views
{
    public interface INutViewLifecycle
    {
        void OnBeforeCreate(INutView view, Type modelType);
        void OnAfterCreate(INutView view, Type modelType);
        void OnBeforeResume(INutView view);
        void OnAfterResume(INutView view);
        void OnBeforePause(INutView view);
        void OnAfterPause(INutView view);
        void OnBeforeStop(INutView view);
        void OnAfterStop(INutView view);
        void OnBeforeDestroy(INutView view);
        void OnAfterDestroy(INutView view);
    }
}