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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SE214L22.View
{
    /// <summary>
    /// Interaction logic for CustomerUserControl.xaml
    /// </summary>
    public partial class CustomerUserControl : UserControl
    {
        public CustomerUserControl()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var command = ((Button)sender).Command;

            if (command.CanExecute(null))
            {
                command.Execute(true);
                new AddCustomerWindow().ShowDialog();
            }
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận thông tin khách hàng?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(true);
                if (btnAfterEdit.Command.CanExecute(null) == true) btnAfterEdit.Command.Execute(null);
            }
            else if (result != MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(false);
            }
        }

        private void btnHiddenCustomer_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận xóa khách hàng?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(true);
                if (btnAfterDele.Command.CanExecute(null) == true) btnAfterDele.Command.Execute(null);
            }
            else if (result != MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(false);
            }
        }
    }
}
