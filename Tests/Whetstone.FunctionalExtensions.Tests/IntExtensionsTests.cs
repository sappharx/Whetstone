using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.FunctionalExtensions.Tests
{
    [TestFixture]
    public class IntExtensionsTests
    {
        [TestCaseSource(typeof(NonNullInputWithMinAndMaxCases))]
        public void GetBoundedValue_HandlesNonNullInputValue(int val, int min, int max, int answer)
        {
            val.GetBoundedValue(min, max)
                .Should().Be(answer, "because GetBoundedValue hanldes non-null input values");
        }

        [TestCaseSource(typeof(NullableInputWithMinCases))]
        public void GetBoundedValue_HandlesNullableInputWithMin(int? val, int def, int min, int answer)
        {
            val.GetBoundedValue(def, min)
                .Should().Be(answer, "because GetBoundedValue handles nullable input values with just minimum");
        }

        [TestCaseSource((typeof (NullableInputWithMinAndMaxCases)))]
        public void GetBoundedValue_HandlesNullableInputWithMinAndMax(int? val, int def, int min, int max, int answer)
        {
            val.GetBoundedValue(def, min, max)
                .Should().Be(answer, "because GetBoundedValues handles nullable input values with minimum and maximum");
        }
    }

    internal class NonNullInputWithMinAndMaxCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] {-1, 0, 2, 0};
            yield return new object[] { 1, 0, 2, 1 };
            yield return new object[] { 3, 0, 2, 2 };
        }
    }

    internal class NullableInputWithMinCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { null, -1, 0, 0 };
            yield return new object[] { null, 1, 0, 1 };
            yield return new object[] { -1, 0, 0, 0 };
            yield return new object[] { 1, 0, 0, 1 };
        }
    }

    internal class NullableInputWithMinAndMaxCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { null, -1, 0, 2, 0 };
            yield return new object[] { null, 1, 0, 2, 1 };
            yield return new object[] { null, 3, 0, 2, 2 };
            yield return new object[] { -1, 0, 0, 2, 0 };
            yield return new object[] { 1, 0, 0, 2, 1 };
            yield return new object[] { 3, 0, 0, 2, 2 };
        }
    }
}
