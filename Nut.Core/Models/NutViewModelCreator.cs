using System;
using Nut.Core.Dependencies;

namespace Nut.Core.Models
{
    public class NutViewModelCreator : INutViewModelCreator
    {
        private readonly INutIocProvider iocProvider;

        public NutViewModelCreator(INutIocProvider iocProvider)
        {
            this.iocProvider = iocProvider;
        }

        public INutViewModel CreateViewModel(Type viewModelType)
        {
            var viewModel = iocProvider.Create(viewModelType) as INutViewModel;
            if (viewModel == null)
            {
                throw new ArgumentException($"ViewModel {viewModelType.Name} should inherit INutViewModel");
            }

            return viewModel;
        }
    }
}