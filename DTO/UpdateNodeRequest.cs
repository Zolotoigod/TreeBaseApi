using System.ComponentModel.DataAnnotations;

namespace TreeBase.DTO
{
    public class UpdateNodeRequest
    {
        [Required]
        public string? TreeName { get; set; }
        [Required]
        public int NodeId { get; set; }
        [Required]
        public string? NewNodeName { get; set; }
    }
}
