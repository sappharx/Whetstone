using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.FunctionalExtensions.Tests
{
    [TestFixture]
    public class FunctionalExtensionsTests
    {
        [Test]
        public void MapTest()
        {
            "123".Map(int.Parse).Should().Be(123, "because Map can handle a static method");

            12.Map(x => x * 2).Should().Be(24, "because Map can handle a lambda expression");

            new[] { 1, 2, 3 }.Map(arr => new List<int>(arr))
                .Should().BeOfType<List<int>>("because Map can handle IEnumerable<T> input");
        }

        [Test]
        public void WhenTest()
        {
            // test overload version where predicate is bool
            "abc".When(true, s => s + s).Should().Be("abcabc", "because predicate is true");
            "abc".When(false, s => s + s).Should().Be("abc", "because predicate is false");

            // test overload version where predicate is Func<bool>
            12.When(() => true, x => x * 2).Should().Be(24, "because predicate evaluates to true");
            13.When(() => false, x => x * 2).Should().Be(13, "because predicate evaluates to false");
            
            // test overload version where predicate is Func<T, bool>
            12.When(x => x % 2 == 0, x => x / 2).Should().Be(6, "because predicate evaluates to true");
            13.When(x => x % 2 == 0, x => x / 2).Should().Be(13, "because predicate evaluates to false");
        }
    }
}
