namespace ProvisionData.Internet
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Domain Names are effectively strings so CompareTo() or StringComparer is preferred over <, >, <=, >=.")]
    public class Label : IEquatable<Label>, IComparable<Label>
    {
        private readonly String _label;

        [SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public Label(String label)
        {
            ThrowIfInvalid(label);

            _label = label;
        }

        public override Int32 GetHashCode()
            => _label?.GetHashCode() ?? 0;

        public override String ToString() => _label;

        public Int32 CompareTo(Label other)
            => String.Compare(_label, other._label, StringComparison.InvariantCultureIgnoreCase);

        public Boolean Equals(Label other)
            => ReferenceEquals(this, other) || _label.Equals(other._label, StringComparison.InvariantCultureIgnoreCase);

        public override Boolean Equals(Object other)
            => other is null ? false : ReferenceEquals(this, other) || (other is Label domainName && Equals(domainName));

        public static implicit operator Label(String domainName) => new Label(domainName);

        public static Label FromString(String domainName) => new Label(domainName);

        public static implicit operator String(Label domainName) => domainName._label;

        public static String FromDomainName(Label domainName) => domainName._label;

        public static Boolean operator ==(Label a, Label b)
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
            return a._label == b._label;
        }

        public static Boolean operator !=(Label a, Label b) => !(a == b);

        private static void ThrowIfInvalid(String label)
        {
            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
            }

            if (String.IsNullOrWhiteSpace(label))
            {
                throw new ArgumentException("Invalid: Must not be Empty or Whitespace", nameof(label));
            }

            if (label.Length > 63)
            {
                throw new ArgumentException("Invalid Length: The length of any one label is limited to between 1 and 63 octets.", nameof(label));
            }
        }
    }
}
