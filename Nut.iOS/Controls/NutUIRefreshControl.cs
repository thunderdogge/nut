using System.Windows.Input;
using Foundation;
using UIKit;

namespace Nut.iOS.Controls
{
    public class NutUIRefreshControl : UIRefreshControl
    {
        private string message;
        private bool isRefreshing;

        public NutUIRefreshControl()
        {
            ValueChanged += (s, e) =>
            {
                var refreshCommand = RefreshCommand;
                if (refreshCommand != null && refreshCommand.CanExecute(null))
                {
                    refreshCommand.Execute(null);
                }
            };
        }

        public ICommand RefreshCommand { get; set; }

        public string Message
        {
            get { return message; }
            set
            {
                message = value ?? string.Empty;
                AttributedTitle = new NSAttributedString(message);
            }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;

                if (isRefreshing)
                {
                    BeginRefreshing();
                }
                else
                {
                    EndRefreshing();
                }
            }
        }
    }
}