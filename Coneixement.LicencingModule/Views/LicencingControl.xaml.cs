using Coneixement.Infrastructure;
using Coneixement.Infrastructure.Helpers.SystemInformationHelper;
using Coneixement.LicencingModule.ViewModals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Coneixement.LicencingModule.Views
{
    /// <summary>
    /// Interaction logic for LicencingControl.xaml
    /// </summary>
    public partial class LicencingControl : UserControl, ILicenceContolView
    {
        public LicencingControl()
        {
            InitializeComponent();
           ViewModel = new LicencingViewModal(this);
            LayoutRoot.AddHandler(RichTextBox.DragOverEvent, new DragEventHandler(LayoutRoot_DragOver), true);
            LayoutRoot.AddHandler(RichTextBox.DropEvent, new DragEventHandler(LayoutRoot_Drop), true);
        }
        private void LayoutRoot_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = false;
        }
        private void LayoutRoot_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);
                var dataFormat = DataFormats.Text;
                if (System.IO.File.Exists(docPath[0]))
                {
                    try
                    {
                        string key = File.ReadAllText(docPath[0]);
                        (ViewModel as LicencingViewModal).SerialKey = key;
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("File could not be opened.");
                    }
                }
            }
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(true)
                (ViewModel as LicencingViewModal).ValidateLicence();
            else
                MessageBox.Show("Enter Licence Key !", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        public IViewModel ViewModel
        {
            get
            {
                return (IViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
