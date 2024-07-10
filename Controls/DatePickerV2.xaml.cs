using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.Controls;

public partial class DatePickerV2 : Grid
{
    public DatePickerV2()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(
        propertyName: nameof(Format),
        returnType: typeof(string),
        declaringType: typeof(DatePickerV2),
        defaultValue: "dd/MM/yyyy",
        defaultBindingMode: BindingMode.TwoWay);
    public static readonly BindableProperty DateProperty = BindableProperty.Create(
        propertyName: nameof(Date),
        returnType: typeof(DateTime),
        declaringType: typeof(DatePickerV2),
        defaultValue: DateTime.Now,
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        propertyName: nameof(Placeholder),
        returnType: typeof(string),
        declaringType: typeof(DatePickerV2),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);
    
    public string Format 
    { 
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }
    
    public DateTime Date
    {
        get => (DateTime)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }
    
    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
}