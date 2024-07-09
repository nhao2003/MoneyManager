using CommunityToolkit.Mvvm.Input;
using MoneyManager.Data.Entities;
using MoneyManager.Services.Category;
using MoneyManager.View;

namespace MoneyManager.ViewModel;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

public partial class CategoryViewModel(ICategoryService categoryService) : BaseViewModel
{
    public ObservableCollection<Category> Categories { get; } = [];

    [RelayCommand]
    public async Task LoadCategories()
    {
        var categories = await categoryService.GetCategoriesAsync();
        Categories.Clear();
        foreach (var category in categories)
        {
            Categories.Add(category);   
        }
    }

    [RelayCommand]
    private async Task AddCategory()
    {
         await Shell.Current.Navigation.PushAsync(new AddCategoryPage());
    }

    [RelayCommand]
    private Task EditCategory(Category category)
    {
        // Navigate to EditCategoryPage with category data (not implemented here)
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task DeleteCategory(Category category)
    {
        Categories.Remove(category);
        categoryService.RemoveCategoryAsync(category.Id);
        return Task.CompletedTask;
    }
}