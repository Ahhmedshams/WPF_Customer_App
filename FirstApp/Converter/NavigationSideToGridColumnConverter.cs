﻿using FirstApp.ViewModel;
using System.Globalization;
using System.Windows.Data;

namespace FirstApp.Converter;

public class NavigationSideToGridColumnConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var navigationSide = (NavigationSide)value;
        return navigationSide == NavigationSide.Left
          ? 0 // <-- Value for Grid.Column
          : 2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
