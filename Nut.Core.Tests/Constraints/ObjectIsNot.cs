using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace Nut.Core.Tests.Constraints
{
    public class ObjectIsNot
    {
        public IResolveConstraint EqualByProperties(object obj)
        {
            return new NotConstraint(new EqualByPropertiesConstraint(obj));
        }

        public IResolveConstraint EqualButNotSameByProperties(object obj, HashSet<Type> immutableTypes)
        {
            return new NotConstraint(new EqualButNotSameByPropertiesConstraint(obj, immutableTypes));
        }
    }
}