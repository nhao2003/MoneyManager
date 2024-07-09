using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.ViewModel;

namespace MoneyManager.View;

public partial class AddEntryPage : ContentPage
{
    public AddEntryPage()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetService<AddEntryViewModel>();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as AddEntryViewModel)?.LoadCategories();
    }
}