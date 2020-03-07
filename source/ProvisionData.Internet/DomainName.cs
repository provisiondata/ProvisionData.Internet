namespace ProvisionData.Internet
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Domain Names are effectively strings so CompareTo() or StringComparer is preferred over <, >, <=, >=.")]
    public class DomainName : IEquatable<DomainName>, IComparable<DomainName>
    {
        public static readonly DomainName Origin = new DomainName("@");

        [SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public DomainName(String name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid domain name.", nameof(name));
            }

            Name = name;
        }

        public String Name { get; }

        public Int32 CompareTo(DomainName other)
            => String.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);

        public override Int32 GetHashCode()
            => Name?.GetHashCode() ?? 0;

        public override String ToString() => Name;

        public Boolean Equals(DomainName other)
            => ReferenceEquals(this, other) || Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);

        public override Boolean Equals(Object other)
            => other is null ? false : ReferenceEquals(this, other) || (other is DomainName domainName && Equals(domainName));

        public static implicit operator DomainName(String domainName) => new DomainName(domainName);

        public static DomainName FromString(String domainName) => new DomainName(domainName);

        public static implicit operator String(DomainName domainName) => domainName.Name;

        public static String FromDomainName(DomainName domainName) => domainName.Name;

        public static Boolean operator ==(DomainName a, DomainName b)
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

        public static Boolean operator !=(DomainName a, DomainName b) => !(a == b);
    }
}
