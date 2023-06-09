namespace Fiorello.Models
{
    public class AboutList:BaseEntity
    {
        public string Name { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
    }
}
