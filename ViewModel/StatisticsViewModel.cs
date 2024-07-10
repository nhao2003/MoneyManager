using System.Collections.ObjectModel;
using MoneyManager.DTOs;
using MoneyManager.Services.Entry;

namespace MoneyManager.ViewModel;

public class Model
{
    public string Country { get; set; }

    public double Counts { get; set; }

    public Model(string name, double count)
    {
        Country = name;
        Counts = count;
    }
}

public class StatisticsViewModel : BaseViewModel
{
    private IEntryService _entryService;

    public StatisticsViewModel(IEntryService entryService)
    {
        _entryService = entryService;
    }

    public ObservableCollection<Model> Data { get; set; } = [];

    private DateTime _selectedDate = DateTime.Now;
    private double _stepValue = 0;

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (_selectedDate != value)
            {
                SetProperty(ref _selectedDate, value);
            }
        }
    }

    public double StepValue
    {
        get => _stepValue;
        set
        {
            if (_stepValue != value)
            {
                SetProperty(ref _stepValue, value);
                UpdateDate();
            }
        }
    }

    private void UpdateDate()
    {
        if (StepValue > 0)
        {
            SelectedDate = SelectedDate.AddMonths(1);
            _ = LoadEntries();
        }
        else if (StepValue < 0)
        {
            SelectedDate = SelectedDate.AddMonths(-1);
            _ = LoadEntries();
        }
        StepValue = 0;
    }
    
    public ObservableCollection<EntryGroup> GroupedEntries { get; set; } = [];

    public async Task LoadEntries()
    {
        Data.Clear();
        var entries = await _entryService.GetEntriesAsync();
        Dictionary<string, double> dict = new();
        foreach (var entry in entries)
        {
            if (entry.Date.Month != SelectedDate.Month || entry.Date.Year != SelectedDate.Year) continue;
            if (dict.ContainsKey(entry.Category.Name))
            {
                dict[entry.Category.Name] += entry.Amount;
            }
            else
            {
                dict[entry.Category.Name] = entry.Amount;
            }
        }
        foreach (var (key, value) in dict)
        {
            Data.Add(new Model(key, value));
        }
        
        var grouped = entries
            .Where(e => e.Date.Month == SelectedDate.Month && e.Date.Year == SelectedDate.Year)
            .GroupBy(e => e.Date)
            .Select(g => new EntryGroup(g.Key, g))
            .ToList();
        GroupedEntries.Clear();
        foreach (var group in grouped)
        {
            GroupedEntries.Add(group);
        }
    }
}