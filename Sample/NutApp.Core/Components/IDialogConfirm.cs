using System;

namespace NutApp.Core.Components
{
    public interface IDialogConfirm
    {
        void Show(string message, Action positiveAction);
        void Show(string message, Action positiveAction, Action negativeAction);
    }
}