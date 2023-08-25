using MusicApp.Models.Entities;
using System.Text.Json.Serialization;

namespace MusicApp.Models.DTOs
{
    public class CommentNewDTO
    {
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
        public string? Text { get; set; }
        public long PostId { get; set; }
        [JsonIgnore]
        public long UserId { get; set; }
    }
}
