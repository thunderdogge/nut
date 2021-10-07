using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace Nut.Core.Tests.Constraints
{
    public class EqualButNotSameByPropertiesConstraint : Constraint
    {
        private readonly object expected;
        private readonly IEnumerable<Type> immutableTypes;
        private readonly bool compareNonPublicMembers;

        public EqualButNotSameByPropertiesConstraint(object expected, IEnumerable<Type> immutableTypes = null, bool compareNonPublicMembers = false)
        {
            this.expected = expected;
            this.immutableTypes = immutableTypes;
            this.compareNonPublicMembers = compareNonPublicMembers;
        }

        public override string Description => "EqualButNotSameByProperties";

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            string failureDescription;
            var isSuccess = new ObjectComparer(immutableTypes).EqualsButNotSame(expected, actual, out failureDescription, compareNonPublicMembers);
            return new ConstraintResult(this, failureDescription, isSuccess);
        }
    }
}