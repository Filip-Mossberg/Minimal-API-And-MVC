using Labb1_Minimal_API.Models.DTOs;

namespace BookUI.Services
{
    public interface IBookService
    {
        public Task<T> GetAllAsync<T>();
        public Task<T> GetByIdAsync<T>(int id);
        public Task<T> NewBookAsync<T>(BookCreateDTO newBook);
        public Task<T> UpdateBookAsync<T>(BookUpdateDTO newBook);
        public Task<T> DeleteBookAsync<T>(int id);
        public Task<T> SearchBookAsync<T>(string searchWord);
    }
}
