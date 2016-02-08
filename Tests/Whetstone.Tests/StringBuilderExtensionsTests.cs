using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.Tests
{
    [TestFixture]
    public class StringBuilderExtensionsTests
    {
        [Test]
        public void AppendFormatedLine_WorksProperly()
        {
            var expected = new StringBuilder()
                .AppendFormat("{0}-{1}", "foo", "bar")
                .AppendLine()
                .ToString();

            new StringBuilder()
                .AppendFormattedLine("{0}-{1}", "foo", "bar")
                .ToString()
                .Should().Be(expected, "because AppendFormattedLine() works");
        }

        [Test]
        public void AppendSequence_WorksProperly()
        {
            new StringBuilder("abc")
                .AppendSequence(new[] {'d', 'e'}, (builder, str) => builder.Append(str))
                .ToString()
                .Should().Be("abcde", "because AppendSeqence() works properly");
        }

        [Test]
        public void RemoveLastCharacter_WorksProperly()
        {
            new StringBuilder("abcdef")
                .RemoveLastCharacter()
                .ToString()
                .Should().Be("abcde", "because RemoveLastCharacter() removes the last character");
        }
    }
}
