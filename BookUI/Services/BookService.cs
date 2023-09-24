using Labb1_Minimal_API.Models.DTOs;

namespace BookUI.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IHttpClientFactory _client;
        public BookService(IHttpClientFactory client) : base(client)
        {
            this._client = client;
        }
        public Task<T> DeleteBookAsync<T>(int id)
        {
            return this.GetAsync<T>(new Models.ApiRequest()
            {
                Apitype = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.BookApiUrl + "/api/book/" + id,
                AccessToken = ""
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return this.GetAsync<T>(new Models.ApiRequest()
            {
                Apitype = StaticDetails.ApiType.GET,
                Url = StaticDetails.BookApiUrl + "/api/book",
                AccessToken = ""
            });
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.GetAsync<T>(new Models.ApiRequest()
            {
                Apitype = StaticDetails.ApiType.GET,
                Url = StaticDetails.BookApiUrl + "/api/book/" + id,
                AccessToken = ""
            });
        }

        public Task<T> NewBookAsync<T>(BookCreateDTO newBook)
        {
            return this.GetAsync<T>(new Models.ApiRequest()
            {
                Apitype = StaticDetails.ApiType.POST,
                Url = StaticDetails.BookApiUrl + "/api/book",
                Data = newBook,
                AccessToken = ""
            });
        }

        public async Task<T> SearchBookAsync<T>(string searchWord)
        {
            return await this.GetAsync<T>(new Models.ApiRequest()
            {
                Apitype = StaticDetails.ApiType.GET,
                Url = StaticDetails.BookApiUrl + "/api/book/" + searchWord,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateBookAsync<T>(BookUpdateDTO newBook)
        {
            return await this.GetAsync<T>(new Models.ApiRequest
            {
                Apitype = StaticDetails.ApiType.PUT,
                Url = StaticDetails.BookApiUrl + "/api/book", 
                Data = newBook,
                AccessToken = ""
            });
        }
    }
}
