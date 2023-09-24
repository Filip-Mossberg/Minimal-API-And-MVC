using BookUI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace BookUI.Services
{
    public class BaseService : IBaseService
    {
        public ApiResponse apiResponse { get; set; }
        public IHttpClientFactory _client { get; set; }
        public BaseService(IHttpClientFactory client)
        {
            this._client = client;
            this.apiResponse = new ApiResponse();
        }

        public async Task<T> GetAsync<T>(ApiRequest request)
        {
            try
            {
                var client = _client.CreateClient();
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);
                client.DefaultRequestHeaders.Clear();


                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject
                        (request.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage respRes = null;
                switch (request.Apitype)
                {
                    case StaticDetails.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                }
                respRes = await client.SendAsync(message);

                var resultString = await respRes.Content.ReadAsStringAsync();
                var resultObj = JsonConvert.DeserializeObject<T>(resultString);

                //if (resultObj != null)
                //{
                //    await Console.Out.WriteLineAsync("Start1");
                //    await Console.Out.WriteLineAsync(resultObj.ToString());
                //}
                //else
                //{
                //    await Console.Out.WriteLineAsync("Start2");
                //    Console.WriteLine("Is empty!");
                //}
                //if (resultString != null)
                //{
                //    await Console.Out.WriteLineAsync("Start3");
                //    await Console.Out.WriteLineAsync(resultString.ToString());
                //}
                //else
                //{
                //    await Console.Out.WriteLineAsync("Start4");
                //    Console.WriteLine("Is empty!");
                //}

                return resultObj;
            }
            catch (Exception exception)
            {
                ApiResponse resp = new ApiResponse
                {
                    IsSuccess = false,
                    StatusMessage = "Failed to get API data!",
                    ErrorMessages = new List<string> { exception.Message }
                };

                var test = JsonConvert.SerializeObject(resp);
                var test2 = JsonConvert.DeserializeObject<T>(test);
                return test2;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
