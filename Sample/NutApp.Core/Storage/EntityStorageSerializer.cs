using System;

namespace NutApp.Core.Storage
{
    public class EntityStorageSerializer : IEntityStorageSerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(byte[] data, Type type)
        {
            throw new NotImplementedException();
        }

        public bool CanDeserialize(Type type)
        {
            return false;
        }
    }
}