using System.ComponentModel.DataAnnotations;

namespace TreeBase.DTO
{
    public class AddNodeRequest
    {
        [Required]
        public string? TreeName { get; set; }
        [Required]
        public string? ParentNodeId { get; set; }
        [Required]
        public string? NodeName { get; set; }
    }
}
