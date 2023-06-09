using Microsoft.EntityFrameworkCore;

namespace Fiorello.Models
{
    [Keyless]
    public class BlogHeader
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
