using BookUI.Models;

namespace BookUI.Services
{
    public interface IBaseService : IDisposable
    {
        public IHttpClientFactory _client { get; set; }

        public Task<T> GetAsync<T>(ApiRequest request);
    }
}
