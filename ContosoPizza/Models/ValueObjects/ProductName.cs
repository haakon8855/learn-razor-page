using System.Text.RegularExpressions;

namespace ContosoPizza.Models.ValueObjects
{
    public class ProductName : ValueObject, IComparable<ProductName>
    {
        public string Value { get; private set; }
        public const string Pattern = @"^[\x00-\xFF]*$";

        public ProductName(string value)
        {
            if (value.Length < 3 || value.Length > 30)
            {
                throw new ArgumentException($"Name must be between 3 and 30 characters, got name with length {value.Length}");
            }
            if (!Regex.IsMatch(value, Pattern))
            {
                throw new ArgumentException($"Name can only be ASCII characters from 0 to 255");
            }
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public int CompareTo(ProductName? other)
        {
            return Value.CompareTo(other?.Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
