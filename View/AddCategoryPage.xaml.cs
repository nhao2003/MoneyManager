using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.ViewModel;

namespace MoneyManager.View;

public partial class AddCategoryPage : ContentPage
{
    public AddCategoryPage()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetService<AddCategoryViewModel>();
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as AddCategoryViewModel)?.Reset();
    }
}