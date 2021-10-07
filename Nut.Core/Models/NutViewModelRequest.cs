using System;
using Nut.Ioc;

namespace Nut.Core.Models
{
    [NutIocIgnore]
    public class NutViewModelRequest
    {
        public Type ViewModelType { get; set; }
        public object ViewModelParameters { get; set; }
        public NutViewModelRequestMode Mode { get; set; }
    }
}