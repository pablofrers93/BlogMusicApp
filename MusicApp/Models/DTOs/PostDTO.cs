using System.Text.Json.Serialization;

namespace MusicApp.Models.DTOs
{
    public class PostDTO
    {
        //public long Id { get; set; }
        //public DateTime CreationDate { get; set; }
        public string title { get; set; }
        //public IFormFile Image { get; set; }
        public string text { get; set; }
        public string category { get; set; }       
    }
}
