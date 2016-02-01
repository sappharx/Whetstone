using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            GetEmptyFluentCollectionFromList<int>()
                .Add(1)
                .Add(2)
                .Count.Should().Be(2, "because 2 items were added");
        }

        [Test]
        public void Clear_HandlesRemovingObjectsFluently()
        {
            GetEmptyFluentCollectionFromList<int>()
                .Add(1)
                .Add(2)
                .Clear()
                .Count.Should().Be(0, "because the collection was cleared");
        }

        [Test]
        public void IsReadOnly_WorksProperly()
        {
            GetEmptyFluentCollectionFromList<int>()
                .IsReadOnly.Should().BeFalse("because the collection isn't read-only");

            new List<int>()
                .Map(list => new ReadOnlyCollection<int>(list))
                .Map(roc => new FluentCollection<int>(roc))
                .IsReadOnly.Should().BeTrue("because the collection is read-only");
        }

        [Test]
        public void Add_HandlesFallingBackToICollection()
        {
            var coll = GetEmptyFluentCollectionFromList<int>();
            ((ICollection<int>) coll).Add(1);
            coll.Count.Should().Be(1, "because ICollection<T>.Add() works");
        }

        [Test]
        public void Clear_HandlesFallingBackToICollection()
        {
            var coll = GetEmptyFluentCollectionFromList<int>();
            ((ICollection<int>) coll).Add(1);
            ((ICollection<int>) coll).Clear();
            coll.Count.Should().Be(0, "because ICollection<T>.Clear() works");
        }

        [Test]
        public void Contains_WorksProperly()
        {
            var collection = GetEmptyFluentCollectionFromList<int>()
                .Add(1);

            collection.Contains(1).Should().BeTrue("because the collections contains 1");
            collection.Contains(2).Should().BeFalse("because the collections doesn't contain 2");
        }

        [Test]
        public void CopyTo_WorksProperly()
        {
            var arr = new[] { 0 };

            GetEmptyFluentCollectionFromList<int>()
                .Add(1)
                .CopyTo(arr, 0);

            arr[0].Should().Be(1, "because the collection was copied to the array successfully");
        }

        [Test]
        public void GetEnumerator_Generic_WorksProperly()
        {
            var enumerator = GetEmptyFluentCollectionFromList<int>()
                .Add(1)
                .GetEnumerator();

            enumerator.MoveNext().Should().BeTrue("because there is another element");
            enumerator.Current.Should().Be(1, "because the enumerator is pointing at 1");
            enumerator.MoveNext().Should().BeFalse("because there are no more elements");
        }

        [Test]
        public void Remove_WorksProperly()
        {
            var collection = GetEmptyFluentCollectionFromList<int>()
                .Add(1);

            collection.Remove(1).Should().BeTrue("because 1 was removed successfully");
            collection.Remove(2).Should().BeFalse("because 2 is not in the collection");
        }

        [Test]
        public void GetEnumerator_NonGeneric_WorksProperly()
        {
            var collection = GetEmptyFluentCollectionFromList<int>()
                .Add(1);
            var enumerator = (collection as IEnumerable).GetEnumerator();

            enumerator.MoveNext().Should().BeTrue("because there is another element");
            enumerator.Current.Should().Be(1, "because the enumerator is pointing at 1");
            enumerator.MoveNext().Should().BeFalse("because there are no more elements");
        }

        private static FluentCollection<T> GetEmptyFluentCollectionFromList<T>()
            => new List<T>()
                .Map(coll => new FluentCollection<T>(coll));
    }
}
