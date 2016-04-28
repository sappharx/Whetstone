using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.Tests
{
    [TestFixture]
    public class DisposableTests
    {
        [Test]
        public void Using_Action_WorksProperly()
        {
            string str = null;

            CreateTestFile();

            Disposable.Using(GetReader, reader => { str = reader.ReadToEnd(); });

            str.Should().Be("foo", "because Using() works properly");
        }

        [Test]
        public void Using_WorksProperly()
        {
            CreateTestFile();

            Disposable.Using(GetReader, reader => reader.ReadToEnd())
                .Should().Be("foo", "because Using() works properly");
        }
        [Test]
        public void Using_2disposables_WorksProperly()
        {
            CreateTestFile();

            Disposable.Using(GetReader, GetReader, (reader1, reader2) => reader1.ReadToEnd() + reader2.ReadToEnd())
                .Should().Be("foofoo", "because Using() works properly");
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
