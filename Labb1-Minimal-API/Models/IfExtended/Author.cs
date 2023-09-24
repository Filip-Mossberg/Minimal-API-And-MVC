using System.ComponentModel.DataAnnotations;

namespace Labb1_Minimal_API.Models.IfExtended
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string Name { get; set; }
    }
}
