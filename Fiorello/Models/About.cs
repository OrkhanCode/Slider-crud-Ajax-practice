namespace Fiorello.Models
{
    public class About : BaseEntity
    {
        public string VideoImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<AboutList> Lists { get; set; }
        public string ListIcon { get; set; }
    }
}
