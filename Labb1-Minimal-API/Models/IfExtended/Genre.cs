using System.ComponentModel.DataAnnotations;

namespace Labb1_Minimal_API.Models.IfExtended
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }
        public string Name { get; set; }
    }
}
