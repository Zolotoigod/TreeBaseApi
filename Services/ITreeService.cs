using TreeBase.DTO;

namespace TreeBase.Services
{
    public interface ITreeService
    {
        Task Add(AddNodeRequest node);
        Task<NodeResponse> GetOrCreateTree(string treeName);
        Task Remove(string treeName, int nodeId);
        Task Update(UpdateNodeRequest request);
    }
}