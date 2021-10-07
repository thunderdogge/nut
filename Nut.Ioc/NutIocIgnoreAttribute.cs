using System;

namespace Nut.Ioc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    public class NutIocIgnoreAttribute : Attribute
    {
    }
}