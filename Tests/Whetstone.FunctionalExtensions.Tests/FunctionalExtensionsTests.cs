using System;
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
            "123".Map(int.Parse).Should().Be(123, "because int.Parse(\"123\") should work");

            12.Map(x => x * 2).Should().Be(24, "because 12 should be doubled to 24");
        }
    }
}
