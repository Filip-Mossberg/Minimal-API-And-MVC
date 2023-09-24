using Labb1_Minimal_API.Data;
using Labb1_Minimal_API.Models;
using Labb1_Minimal_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Labb1_Minimal_API.Services
{
    public class BookRepository : IBookRepository
    {
        private AppDbContext context;
        public BookRepository(AppDbContext db)
        {
            this.context = db;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var Rbook = await context.book.FirstOrDefaultAsync(x => x.Id == id);
            if(Rbook != null)
            {
                context.book.Remove(Rbook);
                await context.SaveChangesAsync();
            }
            
            return Rbook != null ? Rbook : null;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await context.book.ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            if(context.book.FirstOrDefault(b => b.Id == id) != null)
            {
                return await context.book.FirstOrDefaultAsync(b => b.Id == id);
            }
            return null;
        }

        public async Task<Book> NewBook(Book book)
        {
            var Nbook = await context.book.AddAsync(book);
            await context.SaveChangesAsync();
            return Nbook.Entity;
        }

        public async Task<List<Book>> SearchByGenre(string title)
        {
            return await context.book.Where(x => x.Genre.ToLower() == title).ToListAsync();
        }

        public async Task<List<Book>> SearchByAuthor(string author)
        {
            return await context.book.Where(a => a.Author.ToLower() == author).ToListAsync();
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var BookToUpdate = await context.book.FirstOrDefaultAsync(b => b.Id == book.Id);
            if(BookToUpdate != null)
            {
                BookToUpdate.Title = book.Title;
                BookToUpdate.Author = book.Author;
                BookToUpdate.Genre = book.Genre;
                BookToUpdate.IsAvalible = book.IsAvalible;
                BookToUpdate.Description = book.Description;
                await context.SaveChangesAsync();

                return BookToUpdate;
            }
            return null;
        }

        public async Task<IEnumerable<Book>> SearchBook(string searchWord)
        {
            return await context.book.Where
                (b => b.Title.ToLower().Contains(searchWord.ToLower())).ToListAsync();
        }
    }
}
