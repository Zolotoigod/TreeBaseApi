using Microsoft.EntityFrameworkCore;
using TreeBase.DTO;
using TreeBase.ExceptionHandling;
using TreeBase.Mappers;
using TreeBase.Models;
using TreeBase.Repositories.Context;

namespace TreeBase.Services
{
    public class TreeService : ITreeService
    {
        private readonly TreeBaseContext context;
        private readonly ILogger<TreeService> logger;

        public TreeService(TreeBaseContext context, ILogger<TreeService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<NodeResponse> GetOrCreateTree(string treeName)
        {
            var tree = await this.context.Nodes
                .FirstOrDefaultAsync(n => n.ParentId == null && n.Name == treeName);
            if (tree == null)
            {
                tree = new Node() { Name = treeName, TreeName = treeName };
                await context.Nodes.AddAsync(tree);
                await context.SaveChangesAsync();
                logger.LogInformation($"tree with name {treeName} not found, create new tree with id {tree.Id}");
            }

            var cilderns = await context.Nodes
                .Where(n => n.TreeName == treeName)
                .ToListAsync();

            var response = tree.ToService();
            await FindSetCildren(response, cilderns.Select(Mapper.ToService).ToList());
            return response.ToResponse();
        }

        public async Task Add(AddNodeRequest request)
        {
            string message;
            int parentNodeId;
            await CheckTreeExist(request.TreeName!);

            if (!int.TryParse(request.ParentNodeId, out parentNodeId))
            {
                message = $"parrentNodeId is not integer";
                logger.LogError(message);
                throw new SecureException(message);
            }

            var parentNode = await this.context.Nodes
                .FirstOrDefaultAsync(n => n.Id == parentNodeId);
            if (parentNode == null)
            {
                message = $"parrent node not found";
                logger.LogError(message);
                throw new SecureException(message);
            }

            var node = new Node()
            {
                Name = request.NodeName!,
                ParentId = parentNodeId,
                TreeName = request.TreeName!,
            };
           
            await this.context.Nodes.AddAsync(node);
            await this.context.SaveChangesAsync();
        }

        public async Task Remove(string treeName, int nodeId)
        {
            string? message = null;
            await CheckTreeExist(treeName);

            var node = await context.Nodes.FirstOrDefaultAsync(n => n.Id == nodeId);
            var hasChilde = context.Nodes.Any(n => n.ParentId == nodeId);

            if (node == null)
            {
                message = $"node with id {nodeId} not found";
                logger.LogError(message);
                throw new SecureException(message);
            }

            if (node.TreeName != treeName)
            {
                message = $"node with id {nodeId} was found, but belongs to a different tree";
                logger.LogError(message);
                throw new SecureException(message);
            }

            if (hasChilde)
            {
                message = $"you have to delete all children nodes first";
                logger.LogError(message);
                throw new SecureException(message);
            }

            context.Nodes.Remove(node!);
            await this.context.SaveChangesAsync();

            logger.LogInformation($"node with id {nodeId} removed");
        }

        public async Task Update(UpdateNodeRequest request)
        {
            string? message = null;
            await CheckTreeExist(request.TreeName!);
            var node = await context.Nodes.FirstOrDefaultAsync(n => n.Id == request.NodeId);
            if (node == null)
            {
                message = $"node with id {request.NodeId} not found";
                logger.LogError(message);
                throw new SecureException(message);
            }

            context.Nodes.Update(node!);
            node!.Name = request.NewNodeName!;
            await this.context.SaveChangesAsync();

            logger.LogInformation($"node with id {request.NodeId} removed");
        }

        private async Task CheckTreeExist(string treeName)
        {
            string message;
            var tree = await this.context.Nodes
                .FirstOrDefaultAsync(n => n.ParentId == null && n.Name == treeName);
            if (tree == null)
            {
                message = $"tree with name {treeName} not found";
                logger.LogError(message);
                throw new SecureException(message);
            }
        }

        private async Task FindSetCildren(NodeService parent, List<NodeService> nodes)
        {
            var childrens = nodes.Where(n => n.ParentId == parent.Id).ToList();
            parent.Children = childrens;
            foreach (var node in childrens)
            {
                await FindSetCildren(node, nodes);
            }
        }
    }
}
