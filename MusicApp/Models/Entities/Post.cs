using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models.Entities
{
    public class Post
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public long UserId { get; set; }
        public User User{ get; set; }

    }
}
