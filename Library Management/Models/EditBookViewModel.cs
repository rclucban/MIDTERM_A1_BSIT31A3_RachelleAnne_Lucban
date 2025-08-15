namespace Library_Management.Models
{
    public class EditBookViewModel
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public DateTime? PublishedDate { get; set; }


        public string? Author { get; set; }
        public string? AuthorProfileImageUrl { get; set; }
    }
}
