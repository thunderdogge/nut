using System;

namespace Nut.Core.Models
{
    [Flags]
    public enum NutViewModelRequestMode
    {
        Default = 1 << 0,
        NewTask = 1 << 1,
        NewDocument = 1 << 2,
        MultipleTask = 1 << 3,
        SingleTop = 1 << 4,
        ClearTop = 1 << 5,
        ClearStack = 1 << 6,
        NoHistory = 1 << 7,
        NoAnimation = 1 << 8
    }
}