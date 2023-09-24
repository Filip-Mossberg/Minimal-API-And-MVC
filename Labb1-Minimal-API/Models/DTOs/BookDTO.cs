namespace Labb1_Minimal_API.Models.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvalible { get; set; }
        public string Description { get; set; }
    }
}
