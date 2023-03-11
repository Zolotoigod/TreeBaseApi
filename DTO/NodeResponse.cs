namespace TreeBase.DTO
{
    public class NodeResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<NodeResponse>? Children { get; set; } = new List<NodeResponse>();
    }
}
