using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.ViewModel;

namespace MoneyManager.View;

public partial class ManageEntriesPage : ContentPage
{
    public ManageEntriesPage()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetService<ManageEntriesViewModel>();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as ManageEntriesViewModel)?.LoadEntries();
    }
}