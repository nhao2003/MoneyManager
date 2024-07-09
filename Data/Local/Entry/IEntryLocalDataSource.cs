namespace MoneyManager.Data.Local.Entry;

public interface IEntryLocalDataSource
{
    Task<Entities.Entry?> GetEntry(int id);
    Task<Entities.Entry[]> GetEntries();
    Task AddEntry(Entities.Entry entry);
    Task UpdateEntry(Entities.Entry entry);
    Task DeleteEntry(int id);
}