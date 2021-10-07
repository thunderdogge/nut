using System.ComponentModel;
using System.Runtime.CompilerServices;
using Nut.Ioc;

namespace Nut.Core.Navigation
{
    [NutIocIgnore]
    public abstract class NutNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string whichProperty = "")
        {
            var changedArgs = new PropertyChangedEventArgs(whichProperty);
            this.RaisePropertyChanged(changedArgs);
        }

        public virtual void RaisePropertyChanged(PropertyChangedEventArgs changedArgs)
        {
            PropertyChanged?.Invoke(this, changedArgs);
        }
    }
}