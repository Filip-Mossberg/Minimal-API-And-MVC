using Azure;
using BookUI.Models;
using BookUI.Services;
using Labb1_Minimal_API.Models;
using Labb1_Minimal_API.Models.DTOs;
using Labb1_Minimal_API.Services;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookUI.Controllers
{
    public class BookController : Controller
    {
        //public ApiResponse Respons { get; set; }
        public IBookService BookService { get; set; }
        public BookController(IBookService service)
        {
            this.BookService = service;
            //this.Respons = new ApiResponse();
        }
 
        public async Task<IActionResult> BookIndex()
        {
            List<BookDTO> list = new List<BookDTO>();
            var result = await BookService.GetAllAsync<APIResponse>();
            if(result != null && result.IsSuccess)
            {
                // Making sure we are getting the right data in, and 
                // making it into a list of bookDTO
                list = JsonConvert.DeserializeObject<List<BookDTO>>
                    (Convert.ToString(result.Result));
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> NewBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewBook(BookCreateDTO newBook)
        {

            await Console.Out.WriteLineAsync("Genre = " + newBook.Genre);
            await Console.Out.WriteLineAsync("Title = " + newBook.Title);
            await Console.Out.WriteLineAsync("IsAvalible = " + newBook.IsAvalible);

            var response = await BookService.NewBookAsync<APIResponse>(newBook);

            if(response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(BookIndex));
            }
            else
            {
                return RedirectToAction("BookErrors", response);
            }

        }

        public async Task<IActionResult> BookErrors(APIResponse response)
        {
            return View(response);
        }

        public async Task<IActionResult> BookInfo(int id)
        {
            var result = await BookService.GetByIdAsync<APIResponse>(id);
            if(result != null && result.IsSuccess)
            {
                BookDTO book = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(result.Result));
                return View(book);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBook(int id)
        {
            var response = await BookService.GetByIdAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                BookDTO book = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(response.Result));
                return View(book);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookUpdateDTO newBook)
        {
            var response = await BookService.UpdateBookAsync<APIResponse>(newBook);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(BookIndex));
            }
            else
            {
                return RedirectToAction("BookErrors", response);
            }
        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            await BookService.DeleteBookAsync<APIResponse>(id);

            return RedirectToAction(nameof(BookIndex));
        }

        //[Route("/api/book/{content}")]
        public async Task<IActionResult> BookSearch(string content)
        {
            var response = await BookService.SearchBookAsync<APIResponse>(content);

            if(String.IsNullOrEmpty(content))
            {
                RedirectToAction(nameof(BookErrors));
            }

            if(response.IsSuccess)
            {
                var books = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(Convert.ToString(response.Result));
                await Console.Out.WriteLineAsync(books.ToString());
                return View("BookIndex", books);
            }

            return RedirectToAction(nameof(BookErrors), response);
        }
    }
}
