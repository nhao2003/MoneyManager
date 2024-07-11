using System.Collections.ObjectModel;
using System.Diagnostics;
using MoneyManager.Converters;
using MoneyManager.DTOs;
using MoneyManager.Enums;
using MoneyManager.Services.Category;
using MoneyManager.Services.Entry;
using Entry = MoneyManager.Data.Entities.Entry;

namespace MoneyManager.ViewModel;



public class ManageEntriesViewModel
    : BaseViewModel
{
    private readonly IEntryService entryService;
    private readonly ICategoryService categoryService;

    public string[] FilterTypes { get; }

    EnumFilterTypeToStringConverter converter = new();

    public ManageEntriesViewModel(IEntryService entryService, ICategoryService categoryService) 
    {
        this.entryService = entryService;
        this.categoryService = categoryService;
        FilterTypes =
        [
            converter.Convert(FilterType.All, typeof(string), null, null).ToString(),
            converter.Convert(FilterType.Income, typeof(string), null, null).ToString(),
            converter.Convert(FilterType.Expense, typeof(string), null, null).ToString()
        ];
        FilterTypeString = FilterTypes[0];
    }


    private string _filterTypeString;
    
    public string FilterTypeString
    {
        get => _filterTypeString;
        set
        {
            if (_filterTypeString == value) return;
            SetProperty(ref _filterTypeString, value);
            _filterType = (FilterType)converter.ConvertBack(value, typeof(FilterType), null, null);
            _ = LoadEntries();
        }
    }
    
    private FilterType _filterType = FilterType.All;
    
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

        entries = _filterType switch
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
