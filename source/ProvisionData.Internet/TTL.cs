using System;
using System.Globalization;

namespace ProvisionData.Internet
{
    public readonly struct TTL : IEquatable<TTL>, IComparable<TTL>, IComparable
    {
        private readonly Int32 _ttl;

        public TTL(Int32 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _ttl = value;
        }

        public static TTL Parse(String v)
        {
            if (Int32.TryParse(v, out var result))
            {
                return result;
            }

            throw new ArgumentException($"Don't know how to convert \"{v}\" into a TTL.");
        }

        public static implicit operator Int32(TTL ttl) => ttl._ttl;
        public Int32 ToInt32() => _ttl;

        public static implicit operator TTL(Int32 n) => new TTL(n);
        public static TTL FromInt32(Int32 n) => new TTL(n);

        public static Boolean operator <=(TTL a, TTL b) => a._ttl <= b._ttl;
        public static Boolean operator >=(TTL a, TTL b) => a._ttl >= b._ttl;
        public static Boolean operator <(TTL a, TTL b) => a._ttl < b._ttl;
        public static Boolean operator >(TTL a, TTL b) => a._ttl > b._ttl;

        public static Boolean operator !=(TTL a, TTL b) => !(a == b);

        public static Boolean operator ==(TTL a, TTL b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // Return true if the fields match:
            return a._ttl == b._ttl;
        }

        public static implicit operator String(TTL ttl) => ttl._ttl.ToString(CultureInfo.InvariantCulture);

        public Int32 CompareTo(TTL other) => _ttl.CompareTo(other._ttl);

        public Int32 CompareTo(Object obj) => (obj is TTL ttl) ? CompareTo(ttl) : 1;
        //{
        //    if (obj == null)
        //    {
        //        return 1;
        //    }

        //    if (obj is TTL x)
        //    {
        //        return CompareTo(x);
        //    }

        //    throw new ArgumentException("", nameof(obj));
        //}

        public Boolean Equals(TTL other) => _ttl == other._ttl;

        public override Boolean Equals(Object obj)
        {
            while (true)
            {
                if (obj is null)
                {
                    return false;
                }
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }
                if (obj.GetType() != typeof(TTL))
                {
                    return false;
                }
            }
        }

        public override Int32 GetHashCode() => _ttl.GetHashCode();

        public override String ToString() => _ttl.ToString(CultureInfo.InvariantCulture);

        public String HumanReadable()
        {
            var weeks = Math.Floor(_ttl / 604800D);
            var days = Math.Floor((_ttl - (weeks * 604800D)) / 86400D);
            var hours = Math.Floor((_ttl - (weeks * 604800D) - (days * 86400D)) / 3600);
            var minutes = Math.Floor((_ttl - (weeks * 604800D) - (days * 86400D) - (hours * 3600)) / 60);
            var seconds = _ttl - (weeks * 604800D) - (days * 86400D) - (hours * 3600) - (minutes * 60);
            var time = "";
            if (weeks > 0)
            {
                time += weeks + "w";
            }

            if (days > 0)
            {
                time += days + "d";
            }

            if (hours > 0)
            {
                time += hours + "h";
            }

            if (minutes > 0)
            {
                time += minutes + "m";
            }

            if (seconds > 0)
            {
                time += seconds + "s";
            }

            return _ttl > 0 ? time : "0";
        }
    }
}
