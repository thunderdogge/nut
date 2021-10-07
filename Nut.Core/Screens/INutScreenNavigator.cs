namespace Nut.Core.Screens
{
    public interface INutScreenNavigator
    {
        bool NavigateUrl(string url);
        bool NavigateMaps(string query);
        bool NavigateEmail(string email);
        bool NavigateEmail(string email, string subject, string signature);
        bool NavigatePhone(string phone);
        bool NavigateObscure(string value);
        bool CanNavigateUrl(string url);
    }
}