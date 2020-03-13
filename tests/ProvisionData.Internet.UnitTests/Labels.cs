namespace ProvisionData.Internet.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;
    using static FluentAssertions.FluentActions;

    public class Labels
    {
        [Fact]
        public void Must_be_comparable()
        {
            Label a = "example.com.";
            Label b = "example.com.";
            Label c = "apple.com.";
            Label d = "orange.com.";

            a.CompareTo(b).Should().Be(0);
            b.CompareTo(a).Should().Be(0);

            b.CompareTo(c).Should().Be(1);
            b.CompareTo(d).Should().Be(-1);
        }

        [Fact]
        public void Must_be_equatable()
        {
            Label a = "example.com.";
            Label b = "example.com.";
            Label c = "apple.com.";
            Label d = "orange.com.";

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
            String s = new Label("example.com.");
            s.Should().Be("example.com.");
        }

        [Fact]
        public void Must_be_assignable_from_String()
        {
            Label a = "example.com.";
            a.Should().Be("example.com.");
        }

        [Fact]
        public void Must_be_comparable_to_String()
        {
            var d = new Label("example.com.");
            d.CompareTo("example.com.").Should().Be(0);
        }

        [Fact]
        public void Must_throw_ArgumentException_when_domain_is_empty_or_whitespace()
        {
            Invoking(() => new Label("")).Should().Throw<ArgumentException>();

            Invoking(() => new Label("   ")).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Must_throws_ArgumentNullException_when_domain_null()
        {
            Invoking(() => new Label(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
