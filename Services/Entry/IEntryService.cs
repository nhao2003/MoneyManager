using MoneyManager.DTOs;

namespace MoneyManager.Services.Entry;

public interface IEntryService
{
    Task<IEnumerable<Data.Entities.Entry>> GetEntriesAsync();
    
    Task AddEntryAsync(CreateEntryDto category);
    
    Task RemoveEntryAsync(int id);
    
    Task UpdateEntryAsync(int id, UpdateEntryDto category);
}