using System;
using System.Collections.Generic;
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
using ServiceAgregator.Models;

namespace ServiceAgregator.View.DialogWins
{
    /// <summary>
    /// Логика взаимодействия для Default.xaml
    /// </summary>
    public partial class ConfirmOrderWin : Window
    {
        public ConfirmOrderWin(Orders order)
        {
            Order = order;
            this.DataContext = Order;
            InitializeComponent();
        }

        public Orders Order { set; get; }        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
