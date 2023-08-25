using System.Text.Json.Serialization;

namespace MusicApp.Models.DTOs
{
    public class CommentDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Text { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
    }
}
