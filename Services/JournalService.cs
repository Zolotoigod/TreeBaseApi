using Microsoft.EntityFrameworkCore;
using TreeBase.DTO;
using TreeBase.ExceptionHandling;
using TreeBase.Mappers;
using TreeBase.Models;
using TreeBase.Repositories.Context;
using TreeBase.Services;

namespace TreeBase.Repositories
{
    public class JournalService : IJournalService
    {
        private readonly TreeBaseContext context;

        public JournalService(
            TreeBaseContext context)
        {
            this.context = context;
        }

        public async Task Add(LogRecord record)
        {
            await context.LogRecords.AddAsync(record);
            await context.SaveChangesAsync();
        }

        public async Task<LogRecordResponse> GetById(int id)
        {
            var record = await context.LogRecords.FirstOrDefaultAsync();
            if (record == null)
            {
                throw new SecureException($"record with id {id} not found");
            }

            return record.ToFullResponse();
        }

        public async Task<IEnumerable<LogRecordResponse>> Find(RecordFindRequest request)
        {
            var from = request.RecordFilter?.From ?? DateTimeOffset.MinValue;
            var to = request.RecordFilter?.To ?? DateTimeOffset.MaxValue;
            var message = request.RecordFilter?.Search ?? string.Empty;

            var collection = await context.LogRecords
                .Where(r => r.Timestamp > from & r.Timestamp < to & r.Message!.Contains(message))
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync();

            return collection.Select(Mapper.ToResponse);
        }

    }
}
