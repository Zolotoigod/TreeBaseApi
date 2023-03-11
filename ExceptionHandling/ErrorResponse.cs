using System.Runtime.CompilerServices;

namespace TreeBase.ExceptionHandling
{
    public class ErrorResponse
    {
        public string? EventId { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
    }
}
