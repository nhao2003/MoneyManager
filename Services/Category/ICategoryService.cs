using MoneyManager.DTOs;

namespace MoneyManager.Services.Category;

public interface ICategoryService
{
    Task<IEnumerable<Data.Entities.Category>> GetCategoriesAsync();
    
    Task AddCategoryAsync(CreateCategoryDto category);
    
    Task RemoveCategoryAsync(int id);
    
    Task UpdateCategoryAsync(int id, UpdateCategoryDto category);
}