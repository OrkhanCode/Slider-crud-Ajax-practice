namespace Fiorello.Models
{
    public class Discount : BaseEntity
    {
        public byte Percent { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
