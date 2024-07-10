using Microsoft.EntityFrameworkCore;

namespace MoneyManager.Data.Local.Entry;

public enum FilterType
{
    All,
    Income,
    Expense
}

public class EntryLocalDataSource(Database database) : IEntryLocalDataSource
{
    public Task<Entities.Entry?> GetEntry(int id)
    {
        var entry = database.Entries.Include(e => e.Category).FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entry);
    }
    
    public Task<Entities.Entry[]> GetEntries()
    {
        var entries = database.Entries.Include(e => e.Category).OrderByDescending(e => e.Date).ToArray();
        return Task.FromResult(entries);
    }

    public Task AddEntry(Entities.Entry entry)
    {
        database.Entries.Add(entry);
        return Task.FromResult(database.SaveChanges());
    }

    public Task UpdateEntry(Entities.Entry entry)
    {
        database.Entries.Update(entry);
        return Task.FromResult(database.SaveChanges());
    }

    public Task DeleteEntry(int id)
    {
        var entry = database.Entries.Find(id);
        if (entry == null)
        {
            return Task.FromResult(0);
        }
        database.Entries.Remove(entry);
        return Task.FromResult(database.SaveChanges());
    }
}