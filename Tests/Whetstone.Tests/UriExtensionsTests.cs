using System;
using FluentAssertions;
using NUnit.Framework;

namespace Whetstone.FunctionalExtensions.Tests
{
    [TestFixture]
    public class UriExtensionsTests
    {
        [Test]
        public void GetBaseUri_HandlesUriWithQueryParameters()
        {
            new Uri("https://www.google.com/?q=foo").GetBaseUri()
                .Should().Be("https://www.google.com/", "because GetBaseUri() handles URIs with query parametes");
        }

        [Test]
        public void GetBaseUri_HandlesUriWithoutQueryParameters()
        {
            new Uri("https://www.google.com/").GetBaseUri()
                .Should().Be("https://www.google.com/", "because GetBaseUri() handles URIs without query parameters");
        }

        [Test]
        public void QueryStringWithoutQueryDelimiter_HandlesUriWithQueryParameters()
        {
            new Uri("https://www.google.com/?q=foo").QueryStringWithoutQueryDelimiter()
                .Should().Be("q=foo", "because QueryStringWithoutQueryDelimiter() handles URIs with query parameters");
        }

        [Test]
        public void QueryStringWithoutQueryDelimiter_HandlesUriWithoutQueryParameters()
        {
            new Uri("https://www.google.com/").QueryStringWithoutQueryDelimiter()
                .Should().Be(string.Empty, "because QueryStringWithoutQueryDelimiter() handles URIs without query parameters");

            new Uri("https://www.google.com/?").QueryStringWithoutQueryDelimiter()
                .Should().Be(string.Empty, "because QueryStringWithoutQueryDelimiter() handles URIs without query parameters");
        }
    }
}
