using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace SushiGoCompanion.UI.Converters
{
    public class NigiriVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.ToString().Contains("Nigiri"))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
