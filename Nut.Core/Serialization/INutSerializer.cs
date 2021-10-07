using System;

namespace Nut.Core.Serialization
{
    public interface INutSerializer
    {
        string Serialize(object target);
        T Deserialize<T>(string value);
        T DeserializeSafe<T>(string value);
        object Deserialize(Type type, string value);
        object DeserializeSafe(Type type, string value);
    }
}