using System.ComponentModel.DataAnnotations;

namespace Labb1_Minimal_API.Models.IfExtended
{
    public class BookAuthor
    {
        [Key]
        public int BookAuthorID { get; set; }
        public int BookID { get; set; }
        public int AuthorID { get; set; }
    }
}
