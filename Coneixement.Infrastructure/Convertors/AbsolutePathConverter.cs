using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Data;
namespace Coneixement.Infrastructure.Convertors
{
  public  class AbsolutePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (File.Exists(value.ToString()))
                {
                    return value;
                }
                else
                {
                   var  ParentDirectoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                   return Path.Combine(ParentDirectoryPath,value.ToString());
                }
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

