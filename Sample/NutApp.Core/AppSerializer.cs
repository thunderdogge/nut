using System;
using Newtonsoft.Json;
using Nut.Core.Serialization;

namespace NutApp.Core
{
    public class AppSerializer : NutSerializer
    {
        public override string Serialize(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        public override object Deserialize(Type type, string value)
        {
            return JsonConvert.DeserializeObject(value, type);
        }
    }
}