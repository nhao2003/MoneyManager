namespace MoneyManager.Data.Local.Category;

public class CategoryLocalDataSource(Database database) : ICategoryLocalDataSource
{
    
    public Task<Entities.Category?> GetCategory(int id)
    {
        var category = database.Categories.Find(id);
        return Task.FromResult(category);
    }

    public Task<Entities.Category[]> GetCategories()
    {
        var categories = database.Categories.ToArray();
        return Task.FromResult(categories);
    }

    public Task AddCategory(Entities.Category category)
    {
        database.Categories.Add(category);
        return Task.FromResult(database.SaveChanges());
    }

    public Task UpdateCategory(Entities.Category category)
    {
        database.Categories.Update(category);
        return Task.FromResult(database.SaveChanges());
    }

    public Task DeleteCategory(int id)
    {
        var category = database.Categories.Find(id);
        if (category == null)
        {
            return Task.FromResult(0);
        }
        database.Categories.Remove(category);
        return Task.FromResult(database.SaveChanges());
    }
}