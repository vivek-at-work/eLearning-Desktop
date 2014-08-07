using Coneixement.Infrastructure.Helpers;
using System;
using System.IO;
using System.Windows.Data;
namespace Coneixement.Infrastructure.Convertors
{
   public class EncryptedDataConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                FileInfo f = new FileInfo(value.ToString());
                string path = Path.Combine(Path.Combine(StorageHelper.DecryptedDatabase, f.Directory.Name));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                FileEncryption.DecryptFile(value.ToString(),Path.Combine(path ,f.Name));
                return Path.Combine(path, f.Name);
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}

