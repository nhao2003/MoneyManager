using MoneyManager.Data;
using MoneyManager.Data.Local.Entry;
using MoneyManager.DTOs;

namespace MoneyManager.Services.Entry;

public class EntryService : IEntryService
{
    private readonly IEntryLocalDataSource _entryLocalDataSource;
    
    public EntryService(IEntryLocalDataSource entryLocalDataSource)
    {
        _entryLocalDataSource = entryLocalDataSource;
    }
    
    public async Task<IEnumerable<Data.Entities.Entry>> GetEntriesAsync()
    {
        return await _entryLocalDataSource.GetEntries();
    }

    public Task AddEntryAsync(CreateEntryDto createEntryDto)
    {
        var newEntry = new Data.Entities.Entry
        {
            Amount = createEntryDto.Amount,
            Category = createEntryDto.Category,
            Description = createEntryDto.Description,
            Date = createEntryDto.Date,
            IsIncome = createEntryDto.IsIncome,
            ImagePath = createEntryDto.ImagePath
        };
        return _entryLocalDataSource.AddEntry(newEntry);
    }

    public Task RemoveEntryAsync(int id)
    {
        return _entryLocalDataSource.DeleteEntry(id);
    }

    public Task UpdateEntryAsync(int id, UpdateEntryDto category)
    {
        var updatedEntry = new Data.Entities.Entry
        {
            Id = id,
            Amount = category.Amount,
            Category = category.Category,
            Description = category.Description,
            Date = category.Date,
            IsIncome = category.IsIncome,
            ImagePath = category.ImagePath
        };
        return _entryLocalDataSource.UpdateEntry(updatedEntry);
    }
}