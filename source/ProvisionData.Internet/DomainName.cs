namespace ProvisionData.Internet
{
    using System;
    using System.Net;

    public class DomainName
    {
        public static readonly DomainName Empty = new DomainName();

        private readonly String _domain;

        private DomainName() { _domain = String.Empty; }

        public DomainName(String name)
        {
            IsValid(name, throwIfInvalid: true);

            _domain = Normalize(name);
        }

        private static String Normalize(String domain)
            => domain[domain.Length - 1] == '.' ? domain.Substring(0, domain.Length - 1) : domain;

        public override Int32 GetHashCode() => _domain?.GetHashCode() ?? 0;

        public override String ToString() => _domain;

        public Int32 CompareTo(DomainName other)
            => other is null ? 1 : String.Compare(_domain, other._domain, StringComparison.InvariantCultureIgnoreCase);

        public Boolean Equals(DomainName other)
            => other is null ? false : _domain.Equals(other._domain, StringComparison.InvariantCultureIgnoreCase);

        public override Boolean Equals(Object other)
            => other is default(Object) ? false : other is DomainName domainName && Equals(domainName);

        public static implicit operator DomainName(String s) => new DomainName(s);

        public static DomainName FromString(String s) => new DomainName(s);

        public static implicit operator String(DomainName domainName) => domainName._domain;

        public static Boolean operator !=(DomainName a, DomainName b) => !(a == b);

        public static Boolean operator ==(DomainName a, DomainName b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
                return true;

            // If one is null, but not both, return false.
            if ((a is null) || (b is null))
                return false;

            // Return true if the fields match:
            return a._domain == b._domain;
        }

        public static Boolean IsValid(String domain, Boolean throwIfInvalid = false)
        {
            if (domain is null)
                return throwIfInvalid ? throw new ArgumentNullException(nameof(domain)) : false;

            if (String.IsNullOrWhiteSpace(domain))
                return throwIfInvalid ? throw new ArgumentException($"Invalid DomainName ({domain}): Must not be Empty or Whitespace.", nameof(domain)) : false;

            if (domain.Length > 255)
                return throwIfInvalid 
                    ? throw new ArgumentException($"Invalid DomainName ({domain}): The total length of a domain must not exceed 255 octets. The '{domain}' domain is {domain.Length}.", nameof(domain))
                    : false;

            if (domain[0] == '.')
                return throwIfInvalid
                    ? throw new ArgumentException($"Invalid DomainName ({domain}): A domain name MUST NOT start with a period (.).", nameof(domain))
                    : false;

            if (IPAddress.TryParse(domain, out _))
                return true;

            var start = 0;
            var labels = 0;
            do
            {
                var end = domain.IndexOf('.', start);
                if (end == -1)
                    end = domain.Length;

                if (end == start)
                    return throwIfInvalid
                        ? throw new ArgumentException($"Invalid DomainName ({domain}): A domain must not contain two consecutive periods (..)", nameof(domain))
                        : false;

                labels++;
                //var label = domain[start..end];
                if (end - start > 63)
                    return throwIfInvalid
                        ? throw new ArgumentException($"Invalid DomainName ({domain}): The length of any one label is limited to between 1 and 63 octets. '{domain.Substring(start, end - start)}' is {end - start} octets.", nameof(domain))
                        : false;

                start = end + 1;
            } while (start < domain.Length);

            if (labels < 2)
                return throwIfInvalid
                    ? throw new ArgumentException($"Invalid DomainName ({domain}): A domain name must consist of two or more lables separated by a period (.)", nameof(domain))
                    : false;

            return true;
        }
    }
}
