namespace MoneyManager.Data.Local.Category;

public interface ICategoryLocalDataSource
{
    Task<Entities.Category?> GetCategory(int id);
    Task<Entities.Category[]> GetCategories();
    Task AddCategory(Entities.Category category);
    Task UpdateCategory(Entities.Category category);
    Task DeleteCategory(int id);
}