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
    /// Interaction logic for WarrantyOrderUserControl.xaml
    /// </summary>
    public partial class WarrantyOrderUserControl : Window
    {
        public WarrantyOrderUserControl()
        {
            InitializeComponent();
        }

        private void tbPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var command = btnGetCustomer.Command;
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }
            }
        }

        private void btnAddWarrantyOrder_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận thêm yêu cầu bảo hành sản phẩm này?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                try
                {
                    command.Execute(true);
                    MessageBox.Show("Thêm yêu cầu bảo hành thành công!");
                    if (btnReloaWarrantyOrderList.Command.CanExecute(null))
                        btnReloaWarrantyOrderList.Command.Execute(null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (result != MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(false);
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
