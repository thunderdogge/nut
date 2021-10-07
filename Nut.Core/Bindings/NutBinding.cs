using System;
using System.ComponentModel;
using Nut.Core.Bindings.Converters;
using Nut.Ioc;

namespace Nut.Core.Bindings
{
    [NutIocIgnore]
    public class NutBinding : INutBinding
    {
        private INutTargetBinding targetBinding;
        private INutSourceBinding sourceBinding;
        private INutTargetBindingConverter bindingConverter;

        public NutBinding(INutTargetBinding targetBinding,
                          INutSourceBinding sourceBinding,
                          INutTargetBindingConverter bindingConverter)
        {
            this.targetBinding = targetBinding;
            this.sourceBinding = sourceBinding;
            this.bindingConverter = bindingConverter;
        }

        public void Apply(object source)
        {
            if (sourceBinding.DataSource == source)
            {
                return;
            }

            if (sourceBinding.DataSource != null)
            {
                UnsubscribeBindingFromSourceChanges();
                UnsubscribeBindingFromTargetChanges();
            }

            sourceBinding.DataSource = source;

            SetBindingValueFromDataSource();
            SubscribeBindingForSourceChanges();
            SubscribeBindingForTargetChanges();
        }

        private void SetSourceValue(object value)
        {
            if (bindingConverter != null)
            {
                value = bindingConverter.ConvertBack(value);
            }

            sourceBinding.SetValue(value);
        }

        private void SetTargetValue(object value)
        {
            if (bindingConverter != null)
            {
                value = bindingConverter.Convert(value);
            }

            targetBinding.SetValue(value);
        }

        private void SubscribeBindingForSourceChanges()
        {
            var notifyableSource = sourceBinding.BindingSource as INotifyPropertyChanged;
            if (notifyableSource == null)
            {
                return;
            }

            notifyableSource.PropertyChanged += HandleSourcePropertyChanged;
        }

        private void UnsubscribeBindingFromSourceChanges()
        {
            var notifyableSource = sourceBinding.BindingSource as INotifyPropertyChanged;
            if (notifyableSource == null)
            {
                return;
            }

            notifyableSource.PropertyChanged -= HandleSourcePropertyChanged;
        }

        private void HandleSourcePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (sourceBinding.BindingSource != sender)
            {
                return;
            }

            if (sourceBinding.PropertyName != args.PropertyName)
            {
                return;
            }

            SetBindingValueFromDataSource();
        }

        private void SubscribeBindingForTargetChanges()
        {
            if (sourceBinding.Mode != NutBindingMode.TwoWay)
            {
                return;
            }

            targetBinding.SubscribeToEvents();
            targetBinding.ValueChanged += HandleTargetPropertyChanged;
        }

        private void UnsubscribeBindingFromTargetChanges()
        {
            if (sourceBinding.Mode != NutBindingMode.TwoWay)
            {
                return;
            }

            targetBinding.UnsubscribeFromEvents();
            targetBinding.ValueChanged -= HandleTargetPropertyChanged;
        }

        private void HandleTargetPropertyChanged(object sender, NutTargetChangedEventArgs args)
        {
            var targetValue = args.Value;
            SetSourceValue(targetValue);
        }

        private void SetBindingValueFromDataSource()
        {
            var sourceValue = sourceBinding.GetValue();
            SetTargetValue(sourceValue);
        }

        ~NutBinding()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnsubscribeBindingFromTargetChanges();
                UnsubscribeBindingFromSourceChanges();

                targetBinding?.Dispose();
                sourceBinding?.Dispose();

                targetBinding = null;
                sourceBinding = null;
                bindingConverter = null;
            }
        }
    }
}