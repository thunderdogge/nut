using System;
using Nut.Ioc;

namespace Nut.Core.Bindings.Commands
{
    [NutIocIgnore]
    public class NutCommand : NutCommand<object>
    {
        public NutCommand(Action action) : base(_ => action())
        {
        }

        public NutCommand(Action action, Func<bool> canExecute = null) : base(_ => action(), canExecute)
        {
        }
    }

    [NutIocIgnore]
    public class NutCommand<TParameter> : INutCommand
    {
        private readonly Action<TParameter> action;
        private readonly Func<bool> canExecute;

        public NutCommand(Action<TParameter> action) : this(action, null)
        {
        }

        public NutCommand(Action<TParameter> action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public void Execute(object sender)
        {
            if (action == null)
            {
                return;
            }

            if (CanExecute(null))
            {
                action((TParameter) sender);
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public event EventHandler CanExecuteChanged;
    }
}