namespace BookUI
{
    public static class StaticDetails
    {
        public static string BookApiUrl { get; set; }

        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
    }
}
