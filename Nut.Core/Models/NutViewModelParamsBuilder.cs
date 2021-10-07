using System;
using System.Reflection;
using Nut.Core.Serialization;

namespace Nut.Core.Models
{
    public class NutViewModelParamsBuilder : INutViewModelParamsBuilder
    {
        private readonly INutSerializer serializer;

        public NutViewModelParamsBuilder(INutSerializer serializer)
        {
            this.serializer = serializer;
        }

        public object Build(ParameterInfo parameterInfo, string actualParameters)
        {
            var parameterType = parameterInfo.ParameterType;
            if (parameterType.GetTypeInfo().IsValueType)
            {
                return BuildValueType(parameterType, actualParameters);
            }

            return BuildReferenceType(parameterType, actualParameters);
        }

        private object BuildValueType(Type expectedType, string actualParameters)
        {
            if (expectedType == typeof(Guid))
            {
                return Guid.Parse(actualParameters);
            }
            if (expectedType == typeof(int))
            {
                return int.Parse(actualParameters);
            }
            if (expectedType == typeof(bool))
            {
                return bool.Parse(actualParameters);
            }
            if (expectedType == typeof(long))
            {
                return long.Parse(actualParameters);
            }
            if (expectedType == typeof(decimal))
            {
                return decimal.Parse(actualParameters);
            }
            if (expectedType == typeof(double))
            {
                return double.Parse(actualParameters);
            }
            if (expectedType == typeof(byte))
            {
                return byte.Parse(actualParameters);
            }

            throw new ArgumentOutOfRangeException(nameof(expectedType), $"Unexpected value type: {expectedType.Name}");
        }

        private object BuildReferenceType(Type expectedType, string actualParameters)
        {
            if (expectedType == typeof(string))
            {
                return actualParameters;
            }

            return serializer.DeserializeSafe(expectedType, actualParameters);
        }
    }
}