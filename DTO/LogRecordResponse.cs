namespace TreeBase.DTO
{
    public class LogRecordResponse
    {
        public int Id { get; set; }
        public string? EventId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? Message { get; set; }
    }
}
