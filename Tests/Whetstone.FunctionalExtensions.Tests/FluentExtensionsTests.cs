using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.FunctionalExtensions.Tests
{
    [TestFixture]
    public class FluentExtensionsTests
    {
        [Test]
        public void AsFluentCollection_WrapsAnICollectionProperly()
        {
            new List<int> {1, 2, 3, 4}.AsFluentCollection()
                .Should().BeOfType<FluentCollection<int>>("because AsFluentCollection wraps the list properly");
        }
    }
}
