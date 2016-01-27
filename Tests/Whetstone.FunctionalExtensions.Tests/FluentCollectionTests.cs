using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.FunctionalExtensions.Tests
{
    [TestFixture]
    public class FluentCollectionTests
    {
        [Test]
        public void Add_HandlesAddingObjectFluently()
        {
            new FluentCollection<int>(new List<int>())
                .Add(1)
                .Add(2)
                .Count.Should().Be(2, "because 2 items were added");
        }

        [Test]
        public void Clear_HandlesRemovingObjectsFluently()
        {
            new FluentCollection<int>(new List<int>())
                .Add(1)
                .Add(2)
                .Clear()
                .Count.Should().Be(0, "because the collection was cleared");
        }
    }
}
