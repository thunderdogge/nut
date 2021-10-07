using System;
using System.Text.RegularExpressions;
using Nut.Core.Extensions;

namespace Nut.Core.Screens
{
    public abstract class NutScreenNavigator : INutScreenNavigator
    {
        public static Regex UrlSchemePattern = new Regex("^(https?://|ftp://)", RegexOptions.IgnoreCase);
        public static Regex PhoneReplacePattern = new Regex("[^+\\d]");

        public abstract bool NavigateEmail(string email, string subject, string signature);
        public abstract bool CanNavigateUrl(string url);
        protected abstract bool NavigatePhoneNumber(string phone);
        protected abstract bool NavigateUrlWithScheme(string url);

        public bool NavigateUrl(string url)
        {
            var patchedUrl = PrepareUrl(url);
            return NavigateUrlWithScheme(patchedUrl);
        }

        public bool NavigateMaps(string query)
        {
            var q = System.Net.WebUtility.UrlEncode(query);
            return NavigateUrl($"http://maps.google.com/maps?q={q}");
        }

        public bool NavigateEmail(string email)
        {
            return NavigateEmail(email, null, null);
        }

        public virtual bool NavigatePhone(string phone)
        {
            var patchedPhone = PreparePhone(phone);
            if (string.IsNullOrEmpty(patchedPhone))
            {
                OnNavigationFailed();
                return false;
            }

            return NavigatePhoneNumber(patchedPhone);
        }

        public bool NavigateObscure(string value)
        {
            if (value.IsUrlFormat())
            {
                return NavigateUrl(value);
            }

            if (value.IsEmailFormat())
            {
                return NavigateEmail(value);
            }

            if (value.IsPhoneFormat())
            {
                return NavigatePhone(value);
            }

            OnNavigationFailed();
            return false;
        }

        protected virtual string PrepareUrl(string url)
        {
            if (url == null)
            {
                return null;
            }

            var patchedUrl = UrlSchemePattern.IsMatch(url) ? url : $"http://{url}";
            return Uri.EscapeUriString(patchedUrl);
        }

        protected virtual string PreparePhone(string phone)
        {
            if (phone == null)
            {
                return null;
            }

            return PhoneReplacePattern.Replace(phone, String.Empty);
        }

        protected virtual void OnNavigationFailed()
        {
        }
    }
}