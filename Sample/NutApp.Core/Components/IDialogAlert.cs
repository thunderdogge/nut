using System;

namespace NutApp.Core.Components
{
    public interface IDialogAlert
    {
        IDialogAlert Show(string title, string text);
        IDialogAlert Show(string title, string text, string positiveText, Action positiveAction);
        IDialogAlert Show(string title, string text, string positiveText, Action positiveAction, string negativeText, Action negativeAction);
    }
}