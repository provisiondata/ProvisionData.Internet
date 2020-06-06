using FluentAssertions;
using System;
using Xunit;

namespace ProvisionData.Internet.UnitTests
{
    public sealed class Serials : TestBase
    {
        [Fact]
        public void Must_be_creatable_from_UInt32()
        {
            Clock.DateIs(1900, 12, 31);

            var s = Serial.NewSerial(1900998877u);

            s.Should().Be(1900998877);
        }

        [Fact]
        public void Must_be_creatable_from_DateTime()
        {
            Clock.DateIs(1900, 12, 31);

            var s = Serial.NewSerial(Clock.GetCurrentInstant());
            s.Should().Be(1900123100);
        }

        [Fact]
        public void Must_implement_comparison_operators_correctly()
        {
            var a = Serial.NewSerial(1900112200);
            var b = Serial.NewSerial(1900112200);

            (a == b).Should().BeTrue();
            (b == a).Should().BeTrue();

            (a != b).Should().BeFalse();
            (b != a).Should().BeFalse();

            (a >= b).Should().BeTrue();
            (b >= a).Should().BeTrue();

            (a <= b).Should().BeTrue();
            (b <= a).Should().BeTrue();

            (a > b).Should().BeFalse();
            (b > a).Should().BeFalse();

            (a < b).Should().BeFalse();
            (b < a).Should().BeFalse();

            b = Serial.NewSerial(1900112201);

            (a == b).Should().BeFalse();
            (b == a).Should().BeFalse();

            (a != b).Should().BeTrue();
            (b != a).Should().BeTrue();

            (a >= b).Should().BeFalse();
            (b >= a).Should().BeTrue();

            (a <= b).Should().BeTrue();
            (b <= a).Should().BeFalse();

            (a > b).Should().BeFalse();
            (b > a).Should().BeTrue();

            (a < b).Should().BeTrue();
            (b < a).Should().BeFalse();

            // Day
            var c = Serial.NewSerial(1970010100);
            var d = Serial.NewSerial(1970010200);

            (c == d).Should().BeFalse();
            (d == c).Should().BeFalse();

            (c >= d).Should().BeFalse();
            (d >= c).Should().BeTrue();

            (c <= d).Should().BeTrue();
            (d <= c).Should().BeFalse();

            (c > d).Should().BeFalse();
            (d > c).Should().BeTrue();

            (c < d).Should().BeTrue();
            (d < c).Should().BeFalse();

            // Month
            var e = Serial.NewSerial(1970010100);
            var f = Serial.NewSerial(1970020100);

            (e == f).Should().BeFalse();
            (f == e).Should().BeFalse();

            (e >= f).Should().BeFalse();
            (f >= e).Should().BeTrue();

            (e <= f).Should().BeTrue();
            (f <= e).Should().BeFalse();

            (e > f).Should().BeFalse();
            (f > e).Should().BeTrue();

            (e < f).Should().BeTrue();
            (f < e).Should().BeFalse();

            // Year
            var g = Serial.NewSerial(1970010100);
            var h = Serial.NewSerial(1971010100);

            (g == h).Should().BeFalse();
            (h == g).Should().BeFalse();

            (g >= h).Should().BeFalse();
            (h >= g).Should().BeTrue();

            (g <= h).Should().BeTrue();
            (h <= g).Should().BeFalse();

            (g > h).Should().BeFalse();
            (h > g).Should().BeTrue();

            (g < h).Should().BeTrue();
            (h < g).Should().BeFalse();

            // Complex
            var i = Serial.NewSerial(1970123199);
            var j = Serial.NewSerial(1971010100);

            (i == j).Should().BeFalse();
            (j == i).Should().BeFalse();

            (i >= j).Should().BeFalse();
            (j >= i).Should().BeTrue();

            (i <= j).Should().BeTrue();
            (j <= i).Should().BeFalse();

            (i > j).Should().BeFalse();
            (j > i).Should().BeTrue();

            (i < j).Should().BeTrue();
            (j < i).Should().BeFalse();
        }

        [Fact]
        public void Must_output_Human_Readable_strings()
        {
            Clock.DateIs(1900, 12, 31);

            var s = Serial.NewSerial(Clock.GetCurrentInstant());
            s = s.Increment(Clock.GetCurrentInstant());
            s.ToFormattedString().Should().Be("1900-12-31-01");
        }

        [Fact]
        public void Must_increment_when_current_Date_is_less_than_the_Serial_date()
        {
            Clock.DateIs(1970, 01, 01);

            var s = Serial.NewSerial(1974101600u);

            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(1974101601u);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(1974101602u);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(1974101603u);
        }

        [Fact]
        public void Must_increment_when_current_Date_is_equal_the_Serial_date()
        {
            Clock.DateIs(1974, 10, 16);

            var s = Serial.NewSerial(1974101600u);

            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(1974101601u);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(1974101602u);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(1974101603u);
        }

        [Fact]
        public void Must_increment_when_current_Date_is_greater_than_the_Serial_date()
        {
            Clock.DateIs(2000, 01, 01);

            var s = Serial.NewSerial(1974101600u);

            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(2000010100u);

            Clock.DateIs(2000, 01, 02);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(2000010200u);

            Clock.DateIs(2000, 01, 03);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(2000010300u);
        }

        [Fact]
        public void Must_handle_overflow_of_the_sequence()
        {
            Clock.DateIs(1974, 10, 16);

            var s = Serial.NewSerial(1974101699);

            s = s.Increment(Clock.GetCurrentInstant());

            s.Should().Be(1974101700);
        }

        [Fact]
        public void Must_handle_ridiculous_overflow()
        {
            Clock.DateIs(2000, 01, 01);

            var s = Serial.NewSerial(2018093000);

            // Make it overflow
            var last = s;
            for (var i = 0; i <= 10000; i++)
            {
                s = s.Increment(Clock.GetCurrentInstant());

                (s > last).Should().BeTrue();

                last = s;
            }
            s.Should().Be(2019010801);

            // New date, still overflowing
            Clock.DateIs(2000, 01, 02);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(2019010802);

            // No longer overflowing
            Clock.DateIs(2019, 12, 31);
            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(2019123100);
        }

        [Fact]
        public void Must_parse_strings()
        {
            var a = Serial.Parse("2000012345");
            a.Should().Be(2000012345);

            var b = Serial.Parse("2000-01-23-45");
            b.Should().Be(2000012345);

            var c = Serial.Parse("2013031602");
            c.Should().Be(2013031602);

            var e = Record.Exception(() => Serial.Parse(""));
            e.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Must_roll_over_correctly_at_the_end_of_the_month()
        {
            Clock.DateIs(2000, 1, 31);

            var s = Serial.NewSerial(2000013199);

            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(2000020100);
        }

        [Fact]
        public void Must_roll_over_correctly_at_end_the_end_of_the_year()
        {
            Clock.DateIs(2000, 2, 3);

            var s = Serial.NewSerial(1999123100);

            s = s.Increment(Clock.GetCurrentInstant());
            s.Should().Be(2000020300);
        }
    }
}
