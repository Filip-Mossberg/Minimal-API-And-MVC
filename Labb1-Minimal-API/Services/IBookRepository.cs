using Labb1_Minimal_API.Models;
using Labb1_Minimal_API.Models.DTOs;

namespace Labb1_Minimal_API.Services
{
    public interface IBookRepository
    {
        Task<Book> GetById(int id);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> SearchBook(string searchWord);
        Task<Book> NewBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<Book> DeleteBook(int id);
        Task<List<Book>> SearchByGenre(string title);
        Task<List<Book>> SearchByAuthor(string author);
    }
}
