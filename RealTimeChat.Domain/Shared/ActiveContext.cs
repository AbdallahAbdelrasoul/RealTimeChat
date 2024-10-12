namespace RealTimeChat.Domain.Shared
{
    public class ActiveContext
    {
        public int Id { get; set; }
        public int? TenenatId { get; set; }
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public string? SessionId { get; set; }
        public string? Cookie { get; set; }
        public string? DisplayName { get; set; }
        public string? EmailAddress { get; set; }
        public string[]? Roles { get; set; }
    }
}
