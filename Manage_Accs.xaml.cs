using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LC_GUI
{
    /// <summary>
    /// Interaction logic for Manage_Accs.xaml
    /// </summary>
    public partial class Manage_Accs : Window
    {
        public Manage_Accs()
        {
            InitializeComponent();
            var a = IsActiveProperty;

            



        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Debug.WriteLine("Closed");
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
