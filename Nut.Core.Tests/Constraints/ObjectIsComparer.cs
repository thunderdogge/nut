using System;
using System.Collections;
using System.Collections.Generic;

namespace Nut.Core.Tests.Constraints
{
    public class ObjectIsComparer : IComparer
    {
        private readonly bool compareNonPublicMembers;
        private readonly ObjectComparer objectComparer;
        private string failureDescription;
        public string FailureDescription => failureDescription;

        public ObjectIsComparer(IEnumerable<Type> immutableTypes = null, bool compareNonPublicMembers = false)
        {
            this.compareNonPublicMembers = compareNonPublicMembers;
            objectComparer = new ObjectComparer(immutableTypes);
        }

        public int Compare(object left, object right)
        {
            return objectComparer.Equals(left, right, out failureDescription, compareNonPublicMembers) ? 0 : 1;
        }
    }
}