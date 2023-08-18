public class CommentDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Text { get; set; }
    
    }