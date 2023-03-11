namespace TreeBase.Models
{
    public class LogRecord
    {
        public int Id { get; set; }
        public string? EventId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string? HttpMethod { get; set; }
        public string? Path { get; set; }
        public string? Query { get; set; }
        public string? Body { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
    }
}
