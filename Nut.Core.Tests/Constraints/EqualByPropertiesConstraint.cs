using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;

namespace Nut.Core.Tests.Constraints
{
    public class EqualByPropertiesConstraint : Constraint
    {
        private readonly object expected;
        private readonly bool compareNonPublicMembers;
        private readonly IEnumerable<Type> equatableTypes;

        public EqualByPropertiesConstraint(object expected, bool compareNonPublicMembers = false, IEnumerable<Type> equatableTypes = null)
        {
            this.expected = expected;
            this.compareNonPublicMembers = compareNonPublicMembers;
            this.equatableTypes = equatableTypes;
        }

        public override string Description => "EqualByProperties";

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            string failureDescription;
            var isSuccess = new ObjectComparer(equatableTypes: equatableTypes).Equals(expected, actual, out failureDescription, compareNonPublicMembers);
            return new ConstraintResult(this, failureDescription, isSuccess);
        }
    }
}