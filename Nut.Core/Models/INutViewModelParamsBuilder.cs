using System.Reflection;

namespace Nut.Core.Models
{
    public interface INutViewModelParamsBuilder
    {
        object Build(ParameterInfo parameterInfo, string actualParameters);
    }
}