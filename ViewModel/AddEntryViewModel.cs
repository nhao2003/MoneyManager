using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Diagnostics;
using MoneyManager.Data.Entities;
using MoneyManager.DTOs;
using MoneyManager.Services.Category;
using MoneyManager.Services.Entry;

namespace MoneyManager.ViewModel;

public partial class AddEntryViewModel : BaseViewModel
{
    private readonly ICategoryService _categoryService;
    private readonly IEntryService _entryService;

    public AddEntryViewModel(ICategoryService categoryService, IEntryService entryService)
    {
        _categoryService = categoryService;
        _entryService = entryService;
        Categories = new ObservableCollection<Category>();
        Date = DateTime.Now;
    }

    public ObservableCollection<Category> Categories { get; }

    private bool _canSave;
    public bool CanSave
    {
        get => _canSave;
        set => SetProperty(ref _canSave, value);
    }

    private Category? _selectedCategory;
    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            SetProperty(ref _selectedCategory, value);
            CheckCanSave();
        }
    }

    private double _amount;
    public double Amount
    {
        get => _amount;
        set
        {
            SetProperty(ref _amount, value);
            CheckCanSave();
        }
    }

    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set
        {
            SetProperty(ref _description, value);
            CheckCanSave();
        }
    }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    private bool _isIncome = true;
    public bool IsIncome
    {
        get => _isIncome;
        set
        {
            SetProperty(ref _isIncome, value);
            LoadCategories().ConfigureAwait(false);
            SelectedCategory = null;
        }
    }

    public async Task LoadCategories()
    {
        Categories.Clear();
        var categories = await _categoryService.GetCategoriesAsync();
        categories = _isIncome ? categories.Where(c => c.IsIncome).ToList() : categories.Where(c => !c.IsIncome).ToList();
        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    private void CheckCanSave()
    {
        CanSave = SelectedCategory != null && Amount > 0 && !string.IsNullOrEmpty(Description) && Date != default;
    }

    [RelayCommand]
    private async Task SaveEntry()
    {
        if (!CanSave) return;

        var entry = new CreateEntryDto
        {
            Amount = Amount,
            Category = SelectedCategory!,
            Description = Description,
            Date = Date,
            IsIncome = IsIncome,
            ImagePath = "https://picsum.photos/200/300"
        };
        await _entryService.AddEntryAsync(entry);
        Reset();
        Debug.Print("Entry added");
    }

    private void Reset()
    {
        SelectedCategory = null;
        Amount = 0;
        Description = string.Empty;
        Date = DateTime.Now;
        IsIncome = true;
    }

    [RelayCommand]
    private void SelectCategory(Category category)
    {
        SelectedCategory = category;
    }
}
