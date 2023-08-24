﻿using MusicApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models.DTOs
{
    public class CommentNewDTO
    {

        [Required(ErrorMessage = "El campo Text es requerido.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "El campo PostId es requerido.")]
        public long PostId { get; set; }
        //public DateTime CreationDate { get; set; }
        // public long UserId { get; set; }
    }
}
