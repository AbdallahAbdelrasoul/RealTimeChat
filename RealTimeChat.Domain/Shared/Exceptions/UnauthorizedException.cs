namespace RealTimeChat.Domain.Shared.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public int StatusCode { get; set; } = 401;
        public string? HttpResponseMessage { get; set; }
        public UnauthorizedException(string message = "Invalid credentials") : base(message)
        {
            HttpResponseMessage = message;
        }
    }
}
