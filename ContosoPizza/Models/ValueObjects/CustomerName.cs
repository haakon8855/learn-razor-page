using System.Text.RegularExpressions;

namespace ContosoPizza.Models.ValueObjects
{
    public class CustomerName : ValueObject, IComparable<CustomerName>
    {
        public string Value { get; private set; }
        public const string Pattern = @"^[\p{L} .-]*$";

        public CustomerName(string value)
        {
            if (value.Length < 2 || value.Length > 60)
            {
                throw new ArgumentException($"Name must be between 2 and 60 characters, got name with length {value.Length}");
            }
            if (!Regex.IsMatch(value, Pattern))
            {
                throw new ArgumentException($"Name can only contain internation alphabetic characters, periods, hyphens and spaces");
            }
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public int CompareTo(CustomerName? other)
        {
            return Value.CompareTo(other?.Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
