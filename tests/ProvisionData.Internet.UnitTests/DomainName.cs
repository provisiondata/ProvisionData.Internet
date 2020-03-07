namespace ProvisionData.Internet.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class DomainNames
    {
        [Fact]
        public void Must_be_comparable()
        {
            DomainName a = "example.com.";
            DomainName b = "example.com.";
            DomainName c = "apple.com.";
            DomainName d = "orange.com.";

            a.CompareTo(b).Should().Be(0);
            b.CompareTo(a).Should().Be(0);

            b.CompareTo(c).Should().Be(1);
            b.CompareTo(d).Should().Be(-1);
        }

        [Fact]
        public void Must_be_equatable()
        {
            DomainName a = "example.com.";
            DomainName b = "example.com.";
            DomainName c = "apple.com.";
            DomainName d = "orange.com.";

            (a == b).Should().BeTrue();
            (b == a).Should().BeTrue();
            a.Equals(b).Should().BeTrue();
            b.Equals(a).Should().BeTrue();

            (c != d).Should().BeTrue();
            (d != c).Should().BeTrue();
            c.Equals(d).Should().BeFalse();
            d.Equals(c).Should().BeFalse();
        }

        [Fact]
        public void Must_be_assignable_to_String()
        {
            String s = new DomainName("example.com.");
            s.Should().Be("example.com.");
        }

        [Fact]
        public void Must_be_assignable_from_String()
        {
            DomainName a = "example.com.";
            a.Should().Be("example.com.");
        }

        [Fact]
        public void Must_be_comparable_to_String()
        {
            var d = new DomainName("example.com.");
            d.CompareTo("example.com.").Should().Be(0);
        }

        [Fact]
        public void Must_throw_ArgumentException_when_domain_is_empty_or_whitespace()
        {
            var e1 = Record.Exception(() => new DomainName(""));
            e1.Should().BeOfType<ArgumentException>();

            var e2 = Record.Exception(() => new DomainName("   "));
            e2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void Must_throws_ArgumentNullException_when_domain_null()
        {
            var e = Record.Exception(() => new DomainName(null));
            e.Should().BeOfType<ArgumentNullException>();
        }
    }
}
