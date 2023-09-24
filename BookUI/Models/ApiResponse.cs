using System.Net;

namespace BookUI.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; } = true;
        public object Data { get; set; }
        public string StatusMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
