namespace ProvisionData.Internet
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;

    public class FullyQualifiedDomainName : DomainName
    {
        public FullyQualifiedDomainName(String name)
            : base(name)
        {
            ValidateOrThrow(name);
        }

        public Int32 CompareTo(FullyQualifiedDomainName other)
            => other is null ? 1 : String.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);

        public override Int32 GetHashCode() => Name?.GetHashCode() ?? 0;

        public override String ToString() => Name;

        public Boolean Equals(FullyQualifiedDomainName other)
            => other is null ? false : Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);

        public override Boolean Equals(Object other)
            => other is null ? false : ReferenceEquals(this, other) || (other is FullyQualifiedDomainName domainName && Equals(domainName));

        public static implicit operator FullyQualifiedDomainName(String s) => new FullyQualifiedDomainName(s);

        public static new FullyQualifiedDomainName FromString(String s) => new FullyQualifiedDomainName(s);

        public static implicit operator String(FullyQualifiedDomainName domainName) => domainName.Name;

        public static Boolean operator !=(FullyQualifiedDomainName a, FullyQualifiedDomainName b) => !(a == b);

        public static Boolean operator ==(FullyQualifiedDomainName a, FullyQualifiedDomainName b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if ((a is null) || (b is null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.Name == b.Name;
        }

        [SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "We only need English.")]
        public static void ValidateOrThrow(String name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid domain name.", nameof(name));
            }

            if (!name.EndsWith(".", StringComparison.Ordinal) && name != "@" && !IPAddress.TryParse(name, out _))
            {
                throw new ArgumentException($"A fully qualified domain name must end with a period or be a valid IPv4 or IPv6 address. The supplied domain name was: \"{name}\"");
            }
        }
    }
}
