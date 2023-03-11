namespace TreeBase.Helpers
{
    public static class Defines
    {
        public const string FindJornals = "Provides the pagination API. Skip means the number of items should be skipped by server. Take means the maximum number items should be returned by server. All fields of the filter are optional.";
        public const string GetJornal = "Returns the information about an particular event by ID.";
        public const string GetOrCreateTree = "Returns your entire tree. If your tree doesn't exist it will be created automatically.";
        public const string AddNode = "Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.";
        public const string UpdateNode = "Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.";
        public const string RemoveNode = "Delete an existing node in your tree. You must specify a node ID that belongs your tree.";
    }
}
