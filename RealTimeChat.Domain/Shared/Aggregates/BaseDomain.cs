namespace RealTimeChat.Domain.Shared.Aggregates
{
    public class BaseDomain
    {
        public int? TenantId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedUserId { get; set; }
        public int? UpdatedUserId { get; set; }
    }
}
