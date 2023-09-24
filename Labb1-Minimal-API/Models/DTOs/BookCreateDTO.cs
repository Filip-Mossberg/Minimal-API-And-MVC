namespace Labb1_Minimal_API.Models.DTOs
{
    public class BookCreateDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvalible { get; set; }
        public string Description { get; set; }
    }
}
