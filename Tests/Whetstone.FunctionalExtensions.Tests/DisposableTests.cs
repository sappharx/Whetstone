using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.FunctionalExtensions.Tests
{
    [TestFixture]
    public class DisposableTests
    {
        [Test]
        public void Using_WorksProperly()
        {
            CreateTestFile();

            Disposable.Using(GetReader, reader => reader.ReadToEnd())
                .Should().Be("foo", "because Using() works properly");
        }

        private static StreamReader GetReader()
            => new StreamReader("test.txt");

        private static void CreateTestFile()
        {
            using (var writer = new StreamWriter("test.txt"))
            {
                writer.Write("foo");
            }
        }
    }
}
