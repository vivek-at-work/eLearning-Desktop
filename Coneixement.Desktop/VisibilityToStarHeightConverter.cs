using System;
using System.Windows.Data;
using System.Windows;
namespace Coneixement.Desktop
{
    class VisibilityToStarHeightConverter : IValueConverter
    {
        //Implimenting IValue Covertor  Interface to Impliment Ui resolution Independence  
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Collapsed)
            {
                return new GridLength(0, GridUnitType.Star);
            }
            else
            {
                if (parameter == null)
                {
                    throw new ArgumentNullException("parameter");
                }
                return new GridLength(double.Parse(parameter.ToString(), culture), GridUnitType.Star);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
