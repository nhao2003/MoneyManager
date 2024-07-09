using Microsoft.Extensions.Logging;
using MoneyManager.Data;
using MoneyManager.Data.Local.Category;
using MoneyManager.Data.Local.Entry;
using MoneyManager.Services.Category;
using MoneyManager.Services.Entry;
using MoneyManager.ViewModel;
namespace MoneyManager;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddDbContext<Database>();
        builder.Services.AddSingleton<IEntryLocalDataSource, EntryLocalDataSource>();
        builder.Services.AddSingleton<IEntryService, EntryService>();
        builder.Services.AddSingleton<ICategoryLocalDataSource, CategoryLocalDataSource>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<AddCategoryViewModel>();
        builder.Services.AddSingleton<AddEntryViewModel>();
        builder.Services.AddSingleton<CategoryViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}