namespace NutApp.Core.Components
{
    public interface IDialogFactory
    {
        IDialogAlert CreateAlert();
        IDialogConfirm CreateConfirm();
    }
}