using System;
using System.Collections.Generic;
using Nut.Core.Extensions;
using Nut.Core.Platform;

namespace Nut.Core.Models
{
    public class NutViewModelContainer : NutSingleton<INutViewModelContainer>, INutViewModelContainer
    {
        private readonly INutViewModelCreator viewModelCreator;
        private readonly INutViewModelParamsBuilder viewModelParamsBuilder;
        private readonly Dictionary<Guid, INutViewModel> models = new Dictionary<Guid, INutViewModel>();

        public NutViewModelContainer(INutViewModelCreator viewModelCreator,
                                     INutViewModelParamsBuilder viewModelParamsBuilder)
        {
            this.viewModelCreator = viewModelCreator;
            this.viewModelParamsBuilder = viewModelParamsBuilder;
        }

        public INutViewModel GetOrCreateViewModel(Guid viewId, Type viewModelType, string viewModelParameters)
        {
            INutViewModel viewModel;
            if (models.TryGetValue(viewId, out viewModel))
            {
                return viewModel;
            }

            var viewModelName = viewModelType.Name;

            using (Nuts.Trace($"Create `{viewModelName}`"))
            {
                viewModel = CreateViewModel(viewModelType, viewModelParameters);
            }

            using (Nuts.Trace($"Start `{viewModelName}`"))
            {
                viewModel.Start();
            }

            models.Add(viewId, viewModel);

            return viewModel;
        }

        public void RemoveViewModel(Guid viewId)
        {
            models.Remove(viewId);
        }

        private INutViewModel CreateViewModel(Type type, string parameters)
        {
            var viewModel = viewModelCreator.CreateViewModel(type);
            var initMethod = type.GetNamedMethod("Init");
            if (initMethod == null)
            {
                return viewModel;
            }

            var initParameters = initMethod.GetParameters();
            if (initParameters.Length == 0)
            {
                initMethod.Invoke(viewModel, null);
            }

            if (initParameters.Length == 1)
            {
                var initParameter = viewModelParamsBuilder.Build(initParameters[0], parameters);
                initMethod.Invoke(viewModel, new [] { initParameter });
            }

            return viewModel;
        }
    }
}