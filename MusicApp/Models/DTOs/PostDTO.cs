using System.Text.Json.Serialization;

namespace MusicApp.Models.DTOs
{
    public class PostDTO
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
       
       
    }
}
