using static BookUI.StaticDetails;

namespace BookUI.Models
{
    public class ApiRequest
    {
        public ApiType Apitype { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
