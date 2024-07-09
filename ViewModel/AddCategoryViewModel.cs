using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MoneyManager.DTOs;
using MoneyManager.Services.Category;

namespace MoneyManager.ViewModel;

public partial class AddCategoryViewModel(ICategoryService categoryService) : BaseViewModel
{
    
    public void Reset()
    {
        Name = string.Empty;
        SelectedIcon = string.Empty;
        IsIncome = false;
    }
    
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public ObservableCollection<string> Icons { get; } =
    [
        "🍔", "🚗", "💰", "🎁", "🏠", "📱", "🎮", "🎬",
        "🎤", "🎨", "📚", "🎳", "🎯", "🎲", "🎰", "🎳",
        "🎱", "🎭", "🎫", "🎪", "🎧", "🎤", "🎥", "🎦"
    ];
    
    private string _selectedIcon = string.Empty;
    
    public string SelectedIcon
    {
        get => _selectedIcon;
        set => SetProperty(ref _selectedIcon, value);
    }
    
    [RelayCommand]
    private void SelectIcon(string icon)
    {
        SelectedIcon = icon;
    }


    private string _icon = string.Empty;

    public string Icon
    {
        get => _icon;
        set => SetProperty(ref _icon, value);
    }

    private bool _isIncome = true;

    public bool IsIncome
    {
        get => _isIncome;
        set => SetProperty(ref _isIncome, value);
    }

    private bool IsAddCategoryEnabled => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(SelectedIcon);

    [RelayCommand]
    private async Task SaveCategory()
    {
        if (!IsAddCategoryEnabled)
        {
            return;
        }
        var category = new CreateCategoryDto()
        {
            Name = Name,
            Icon = SelectedIcon,
            IsIncome = IsIncome
        };
        await categoryService.AddCategoryAsync(category);
        await Shell.Current.Navigation.PopAsync();
    }
}