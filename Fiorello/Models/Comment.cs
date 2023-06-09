namespace Fiorello.Models
{
    public class Comment:BaseEntity
    {
        public string Image { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
    }
}
