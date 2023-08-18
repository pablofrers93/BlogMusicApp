public class PostDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
       
       
    }