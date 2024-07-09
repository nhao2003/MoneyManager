using MoneyManager.Data.Local.Category;
using MoneyManager.DTOs;

namespace MoneyManager.Services.Category;

public class CategoryService(ICategoryLocalDataSource categoryLocalDataSource) : ICategoryService
{
    private ICategoryLocalDataSource _categoryLocalDataSource = categoryLocalDataSource;

    public async Task<IEnumerable<Data.Entities.Category>> GetCategoriesAsync()
    {
        var categories = await _categoryLocalDataSource.GetCategories();
        return categories;
    }

    public Task AddCategoryAsync(CreateCategoryDto category)
    {
        var newCategory = new Data.Entities.Category
        {
            Name = category.Name,
            Icon = category.Icon,
            IsIncome = category.IsIncome
        };
        return _categoryLocalDataSource.AddCategory(newCategory);
    }

    public Task RemoveCategoryAsync(int id)
    {
        return _categoryLocalDataSource.DeleteCategory(id);
    }

    public Task UpdateCategoryAsync(int id, UpdateCategoryDto category)
    {
        var updatedCategory = new Data.Entities.Category
        {
            Id = id,
            Name = category.Name,
            Icon = category.Icon
        };
        return _categoryLocalDataSource.UpdateCategory(updatedCategory);
    }
}