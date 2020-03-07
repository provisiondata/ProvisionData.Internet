namespace ProvisionData.Internet.UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    public class FullyQualifiedDomainNames
    {
        [Fact]
        public void Must_be_comparable()
        {
            FullyQualifiedDomainName a = "example.com.";
            FullyQualifiedDomainName b = "example.com.";
            FullyQualifiedDomainName c = "apple.com.";
            FullyQualifiedDomainName d = "orange.com.";

            a.CompareTo(b).Should().Be(0);
            b.CompareTo(a).Should().Be(0);

            b.CompareTo(c).Should().Be(1);
            b.CompareTo(d).Should().Be(-1);
        }

        [Fact]
        public void Must_be_equatable()
        {
            FullyQualifiedDomainName a = "example.com.";
            FullyQualifiedDomainName b = "example.com.";
            FullyQualifiedDomainName c = "apple.com.";
            FullyQualifiedDomainName d = "orange.com.";

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
            String s = new FullyQualifiedDomainName("example.com.");
            s.Should().Be("example.com.");
        }

        [Fact]
        public void Must_be_assignable_from_String()
        {
            FullyQualifiedDomainName a = "example.com.";
            a.Should().Be("example.com.");
        }

        [Fact]
        public void Must_be_comparable_to_String()
        {
            var d = new FullyQualifiedDomainName("example.com.");
            d.CompareTo("example.com.").Should().Be(0);
        }

        [Fact]
        public void Must_throw_ArgumentException_when_domain_is_empty_or_whitespace()
        {
            var e1 = Record.Exception(() => new FullyQualifiedDomainName(""));
            e1.Should().BeOfType<ArgumentException>();

            var e2 = Record.Exception(() => new FullyQualifiedDomainName("   "));
            e2.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void Must_throw_ArgumentNullException_when_domain_null()
        {
            var e = Record.Exception(() => new FullyQualifiedDomainName(null));
            e.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Must_throw_ArgumentException_when_domain_does_not_have_trailing_dot()
        {
            var e = Record.Exception(() => new FullyQualifiedDomainName("example.com"));
            e.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        public void Must_accept_IPv4_or_IPv6_addresses()
        {
            FullyQualifiedDomainName a = "192.168.1.1";
            FullyQualifiedDomainName b = "2001:0db8:0000:0000:0000:ff00:0042:8329";
            FullyQualifiedDomainName c = "2001:db8:0:0:0:ff00:42:8329";
            FullyQualifiedDomainName d = "2001:db8::ff00:42:8329";
        }
    }
}
