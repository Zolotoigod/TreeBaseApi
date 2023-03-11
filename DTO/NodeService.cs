using TreeBase.Models;

namespace TreeBase.DTO
{
    public class NodeService
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Name { get; set; }
        public ICollection<NodeService>? Children { get; set; } = new List<NodeService>();
    }
}
