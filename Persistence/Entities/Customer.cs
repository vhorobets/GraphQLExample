namespace Persistence.Entities
{
    public enum CustomerTypeEnum : byte
    {
        Normal = 1,
        Gold = 2,
        Premium = 3
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public CustomerTypeEnum CustomerType { get; set; }
    }
}
