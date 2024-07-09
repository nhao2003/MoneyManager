using MoneyManager.Services.Category;
using MoneyManager.Services.Entry;

namespace MoneyManager.ViewModel;

public class ManageEntriesViewModel : BaseViewModel
{
    IEntryService _entryService;
    ICategoryService _categoryService;
    
    public ManageEntriesViewModel(IEntryService entryService, ICategoryService categoryService)
    {
        _entryService = entryService;
        _categoryService = categoryService;
    }
    
 
}