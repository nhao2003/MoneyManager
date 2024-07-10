using System.Collections.ObjectModel;
using System.Diagnostics;
using MoneyManager.DTOs;
using MoneyManager.Enums;
using MoneyManager.Services.Category;
using MoneyManager.Services.Entry;
using Entry = MoneyManager.Data.Entities.Entry;

namespace MoneyManager.ViewModel;



public class ManageEntriesViewModel(IEntryService entryService, ICategoryService categoryService)
    : BaseViewModel
{
    
    private FilterType _filterType = FilterType.All;
    
    public FilterType FilterType
    {
        get => _filterType;
        set
        {
            if (_filterType == value) return;
            SetProperty(ref _filterType, value);
            _ = LoadEntries();
        }
    }
    
    // First day of the month
    private DateTime _startDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
    
    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (_startDate == value) return;
            if (value > EndDate)
            {
                Debug.WriteLine("Start date cannot be greater than end date");
                return;
            }
            SetProperty(ref _startDate, value);
            _ = LoadEntries();
        }
    }
    
    // Last day of the month
    private DateTime _endDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
    
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            if (_endDate == value) return;
            if (value < StartDate)
            {
                Debug.WriteLine("End date cannot be less than start date");
                return;
            }
            SetProperty(ref _endDate, value);
            _ = LoadEntries();
        }
    }
    
    public ObservableCollection<EntryGroup> GroupedEntries { get; set; } = [];
    public ObservableCollection<Model> Data { get; set; } = [];

    public async Task LoadEntries()
    {
        var entries = await entryService.GetEntriesAsync();

        entries = FilterType switch
        {
            FilterType.Income => entries.Where(e => e.IsIncome),
            FilterType.Expense => entries.Where(e => !e.IsIncome),
            _ => entries.Where(e => e.Date.Date >= StartDate.Date && e.Date.Date <= EndDate.Date)
        };

        // Grouping entries by date and creating EntryGroup objects
        var enumerable = entries as Entry[] ?? entries.ToArray();
        var grouped = enumerable
            .GroupBy(e => e.Date.Date)
            .Select(g => new EntryGroup(g.Key, g))
            .ToList();

        // Clear and update GroupedEntries
        GroupedEntries.Clear();
        foreach (var group in grouped)
        {
            GroupedEntries.Add(group);
        }

        // Summarizing data by category
        var dict = enumerable
            .GroupBy(e => e.Category.Name)
            .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

        // Clear and update Data
        Data.Clear();
        foreach (var (key, value) in dict)
        {
            Data.Add(new Model(key, value));
        }
    }
}