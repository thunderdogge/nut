using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace Nut.Core.Tests.Constraints
{
    public static class ObjectIs
    {
        public static ObjectIsNot Not { get; } = new ObjectIsNot();

        public static IResolveConstraint ArrayEqualByProperties<T>(IEnumerable<T> enumerable, bool compareNonPublicMembers = false)
        {
            return new ObjectIsCollectionEquivalentConstraint(enumerable, compareNonPublicMembers);
        }

        public static IResolveConstraint EqualByProperties(object obj, bool compareNonPublicMembers = false, IEnumerable<Type> equatableTypes = null)
        {
            return new EqualByPropertiesConstraint(obj, compareNonPublicMembers, equatableTypes);
        }

        public static IResolveConstraint EqualButNotSameByProperties(object obj, IEnumerable<Type> immutableTypes = null, bool compareNonPublicMembers = false)
        {
            return new EqualButNotSameByPropertiesConstraint(obj, immutableTypes, compareNonPublicMembers);
        }
    }
}