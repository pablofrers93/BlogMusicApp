using MusicApp.Models.Entities;
using System.Text.Json.Serialization;

namespace MusicApp.Models.DTOs
{
public class UserDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}