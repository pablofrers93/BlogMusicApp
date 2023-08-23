﻿using MusicApp.Models.Entities;

namespace MusicApp.Models.DTOs
{
    public class CommentNewDTO
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Text { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
    }
}