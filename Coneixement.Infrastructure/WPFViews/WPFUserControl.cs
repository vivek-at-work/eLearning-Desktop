using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Coneixement.Infrastructure.WPFViews
{
  public  class WPFUserControl : UserControl
    {
        public WPFUserControl()
        {
            Image finalImage = new Image();
            BitmapImage background = new BitmapImage();
            background.BeginInit();
            background.UriSource = new Uri("pack://application:,,,/Coneixement.Infrastructure;component/Images/ikon logo copy-1.jpg");
            background.EndInit();
            this.Background = new ImageBrush(background);
        }
    }
}
