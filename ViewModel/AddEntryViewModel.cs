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
    private ICategoryService _categoryService;
    private IEntryService _entryService;

    public AddEntryViewModel(ICategoryService categoryService, IEntryService entryService)
    {
        _categoryService = categoryService;
        _entryService = entryService;
    }

    public async Task LoadCategories()
    {
        Categories.Clear();
        foreach (var category in await _categoryService.GetCategoriesAsync())
        {
            Categories.Add(category);
        }
    }
    
    private bool CanSave => SelectedCategory != null && Amount > 0 && !string.IsNullOrEmpty(Description) && Date != null;
    
    [RelayCommand]
    private async Task SaveEntry()
    {
        if (CanSave)
        {
            var entry = new CreateEntryDto()
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
    }
    
    private void Reset()
    {
        SelectedCategory = null;
        Amount = 0;
        Description = "";
        Date = DateTime.Now;
        IsIncome = true;
    }
    
    public ObservableCollection<Category> Categories { get; } = [];
    

    private Category? _selectedCategory;

    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set => SetProperty(ref _selectedCategory, value);
    }

    private double _amount;

    public double Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }
    
    private string _description;
    
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
    
    private DateTime _date = DateTime.Now;
    
    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }
    
    private bool _isIncome = true;
    
    public bool IsIncome
    {
        get => _isIncome;
        set => SetProperty(ref _isIncome, value);
    }
    
    [RelayCommand]
    private void SelectCategory(Category category)
    {
        SelectedCategory = category;
    }
}