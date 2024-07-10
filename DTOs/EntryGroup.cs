using System.Collections.ObjectModel;
namespace MoneyManager.DTOs;

public class EntryGroup(DateTime date, IEnumerable<Data.Entities.Entry> entries) : ObservableCollection<Data.Entities.Entry>(entries)
{
    public DateTime Date { get; set; } = date;
}