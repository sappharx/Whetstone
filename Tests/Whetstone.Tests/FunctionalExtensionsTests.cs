using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.Tests
{
    [TestFixture]
    public class FunctionalExtensionsTests
    {
        #region Map() tests

        [Test]
        public void Map_HandlesStaticMethods()
        {
            "123".Map(int.Parse).Should().Be(123, "because Map can handle a static method");
        }

        [Test]
        public void Map_HandlesLambdaExpressions()
        {
            12.Map(x => x * 2).Should().Be(24, "because Map can handle a lambda expression");
        }

        [Test]
        public void Map_HandlesMappingAnIEnumberableToAnotherType()
        {
            new[] {1, 2, 3}.Map(arr => arr.Aggregate((a, b) => a + b))
                .Should().Be(6, "because Map can map an IEnumerable<T> to a non-enumerable type");

            new[] {'a', 'b', 'c'}.Map(arr => new string(arr))
                .Should().Be("abc", "because Map can map an IEnumerable<char> to string");

            new[] { 1, 2, 3 }.Map(arr => new List<int>(arr))
                .Should().BeOfType<List<int>>("because Map can map an IEnumerable<T> to another IEnumerable<T> implementation");
        }

        #endregion

        #region MapAsync() tests

        [Test]
        public async Task MapAsync_HandlesStaticMethods()
        {
            (await "123".MapAsync(int.Parse)).Should().Be(123, "because MapAsync can handle a static method");
        }

        [Test]
        public async Task MapAsync_HandlesLambdaExpressions()
        {
            (await 12.MapAsync(x => x * 2)).Should().Be(24, "because MapAsync can handle a lambda expression");
        }

        [Test]
        public async Task MapAsync_HandlesMappingAnIEnumberableToAnotherType()
        {
            (await new[] { 1, 2, 3 }.MapAsync(arr => arr.Aggregate((a, b) => a + b)))
                .Should().Be(6, "because MapAsync can map an IEnumerable<T> to a non-enumerable type");

            (await new[] { 'a', 'b', 'c' }.MapAsync(arr => new string(arr)))
                .Should().Be("abc", "because MapAsync can map an IEnumerable<char> to string");

            (await new[] { 1, 2, 3 }.MapAsync(arr => new List<int>(arr)))
                .Should().BeOfType<List<int>>("because MapAsync can map an IEnumerable<T> to another IEnumerable<T> implementation");
        }

        #endregion

        #region Tee() tests

        [Test]
        public void Tee_ReturnsTheInputObject()
        {
            "Hello".Tee(Console.WriteLine).Should().Be("Hello", "because Tee returns the input object");
        }

        [Test]
        public void Tee_PerformsActions()
        {
            var destination = string.Empty;
            Action<string> copy = s => destination = s;

            "Hello".Tee(copy);
            destination.Should().Be("Hello", "because Tee performs the provided action");
        }

        #endregion

        #region TeeAsync() tests

        [Test]
        public async Task TeeAsync_ReturnsTheInputObject()
        {
            (await "Hello".TeeAsync(Console.WriteLine))
                .Should().Be("Hello", "because TeeAsync returns the input object");
        }

        [Test]
        public async Task TeeAsync_PerformsActions()
        {
            var destination = string.Empty;
            Action<string> copy = s => destination = s;

            await "Hello".TeeAsync(copy);
            destination.Should().Be("Hello", "because TeeAsync performs the provided action");
        }

        #endregion

        #region When() tests

        [Test]
        public void When_HandlesBooleanParameter()
        {
            // test overload version where predicate is bool
            "abc".When(true, s => s + s).Should().Be("abcabc", "because predicate is true");
            "abc".When(false, s => s + s).Should().Be("abc", "because predicate is false");
        }

        [Test]
        public void When_HandlesFuncParameterWithNoParameter()
        {
            // test overload version where predicate is Func<bool>
            12.When(() => true, x => x * 2).Should().Be(24, "because predicate evaluates to true");
            13.When(() => false, x => x * 2).Should().Be(13, "because predicate evaluates to false");
        }

        [Test]
        public void When_HandlesFuncParameterWithParamterOfTypeT()
        {
            // test overload version where predicate is Func<T, bool>
            12.When(x => x % 2 == 0, x => x / 2).Should().Be(6, "because predicate evaluates to true");
            13.When(x => x % 2 == 0, x => x / 2).Should().Be(13, "because predicate evaluates to false");
        }

        #endregion
    }
}
