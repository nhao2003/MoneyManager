using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyManager.ViewModel;

namespace MoneyManager.View;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage()
    {
        InitializeComponent();
        BindingContext = IPlatformApplication.Current?.Services.GetService<StatisticsViewModel>();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as StatisticsViewModel)?.LoadEntries();
    }
}