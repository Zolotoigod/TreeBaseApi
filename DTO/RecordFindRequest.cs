using System.ComponentModel.DataAnnotations;

namespace TreeBase.DTO
{
    public class RecordFindRequest
    {
        [Required]
        public int Skip { get; set; }
        [Required]
        public int Take { get; set; }
        [Required]
        public RecordFilter? RecordFilter { get; set; }
    }

    public class RecordFilter
    {
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public string? Search { get; set; }
    }
}
