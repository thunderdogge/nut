using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace Nut.Core.Tests.Constraints
{
    public class ObjectIsCollectionEquivalentConstraint: CollectionEquivalentConstraint
    {
        private readonly ObjectIsComparer objectIsComparer;

        public ObjectIsCollectionEquivalentConstraint(IEnumerable expected, bool compareNonPublicMembers = false) : base(expected)
        {
            objectIsComparer = new ObjectIsComparer(compareNonPublicMembers: compareNonPublicMembers);
            Using(objectIsComparer);
        }

        public override string Description => "ObjectIsCollectionEquivalent";

        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            var collection = actual as IEnumerable;
            if (collection == null)
            {
                throw new ArgumentException("The actual value must be an IEnumerable", "actual");
            }
            return new ConstraintResult(this, objectIsComparer.FailureDescription, Matches(collection));
        }
    }
}