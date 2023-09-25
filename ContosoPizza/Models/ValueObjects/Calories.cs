namespace ContosoPizza.Models.ValueObjects
{
    public class Calories : ValueObject, IComparable<ProductName>
    {
        public decimal Value { get; private set; }

        public Calories(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Calories cannot be negative");
            }
            else if (value >= 10000)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Calories cannot be greater than 10000");
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
            return Value.ToString();
        }

        public decimal ToDecimal()
        {
            return Value;
        }
    }
}
