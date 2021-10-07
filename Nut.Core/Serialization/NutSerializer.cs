using System;
using Nut.Core.Platform;

namespace Nut.Core.Serialization
{
    public abstract class NutSerializer : NutSingleton<INutSerializer>, INutSerializer
    {
        public abstract string Serialize(object target);

        public abstract object Deserialize(Type type, string value);

        public virtual T Deserialize<T>(string value)
        {
            return (T) Deserialize(typeof(T), value);
        }

        public virtual T DeserializeSafe<T>(string value)
        {
            try
            {
                return Deserialize<T>(value);
            }
            catch
            {
                return default(T);
            }
        }

        public virtual object DeserializeSafe(Type type, string value)
        {
            try
            {
                return Deserialize(type, value);
            }
            catch
            {
                return null;
            }
        }
    }
}