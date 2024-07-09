using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.ViewModel;

namespace MoneyManager.View;

public partial class CategoryPage : ContentPage
{
    public CategoryPage()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetService<CategoryViewModel>();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as CategoryViewModel)?.LoadCategories();
    }
}