using TreeBase.DTO;
using TreeBase.Models;

namespace TreeBase.Mappers
{
    public static class Mapper
    {
        private const string FullLogTemplate =
            "#Type: {0} #Method: {1} #Path: {2} #Query: {3} #Body: {4} #Message: {5} #StackTrace: {6}";
        public static NodeService ToService(this Node node) =>
            new NodeService()
            {
                Id = node.Id,
                Name = node.Name,
                ParentId = node.ParentId,
            };

        public static NodeResponse ToResponse(this NodeService node) =>
            new NodeResponse()
            {
                Id = node.Id,
                Name = node.Name,
                Children = node.Children?.Select(Mapper.ToResponse).ToList(),
            };

        public static LogRecordResponse ToResponse(this LogRecord record) =>
            new LogRecordResponse() 
            {
                Id = record.Id,
                EventId = record.EventId,
                CreatedAt = record.Timestamp,
                Message = record.Message,
            };

        public static LogRecordResponse ToFullResponse(this LogRecord record) =>
            new LogRecordResponse()
            {
                Id = record.Id,
                EventId = record.EventId,
                CreatedAt = record.Timestamp,
                Message = string.Format(FullLogTemplate,
                    record.Type,
                    record.HttpMethod,
                    record.Path,
                    record.Query,
                    record.Body,
                    record.Message,
                    record.StackTrace),
            };
    }
}
