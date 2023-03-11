using TreeBase.DTO;
using TreeBase.Models;

namespace TreeBase.Services
{
    public interface IJournalService
    {
        Task Add(LogRecord record);
        Task<IEnumerable<LogRecordResponse>> Find(RecordFindRequest request);
        Task<LogRecordResponse> GetById(int id);
    }
}