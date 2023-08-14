using System;
using System.Globalization;
using System.Windows.Data;

namespace EpicrisisWord.Core.Navigations;

internal class ViewModelConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            Type ViewModelType = value.GetType();
            string? ViewNameSpace = ViewModelType.Namespace?.Replace("ViewModel", "View");
            string? ClassName = ViewModelType?.Name.Replace("Model", string.Empty);
            Type? ViewType = Type.GetType(string.Format("{0}.{1}", ViewNameSpace, ClassName));
            if (ViewType != null) return Activator.CreateInstance(ViewType)!;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
