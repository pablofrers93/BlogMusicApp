﻿namespace MusicApp.Models.Entities
{
    public class Comment
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Text { get; set; }
        public Post? Post { get; set; }
        public User? User { get; set; }
    }
}
