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
    /// Interaction logic for ManagementOrderUserControl.xaml
    /// </summary>
    public partial class ManagementOrderUserControl : UserControl
    {
        public ManagementOrderUserControl()
        {
            InitializeComponent();
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            var command = ((Button)sender).Command;
            if (command.CanExecute(null))
            {
                command.Execute(true);
                var window = new AddOrderWindow();
                window.ShowDialog();
            }

        }

        private void itemWaitForSent_Click(object sender, RoutedEventArgs e)
        {
            var command = itemWaitForSent.Command;
            if (command.CanExecute(null))
                command.Execute(0);
        }

        private void itemSent_Click(object sender, RoutedEventArgs e)
        {
            var command = itemSent.Command;
            if (command.CanExecute(null))
                command.Execute(1);
        }

        private void itemDone_Click(object sender, RoutedEventArgs e)
        {
            var command = itemDone.Command;
            if (command.CanExecute(null))
                command.Execute(2);
        }

        private void menuItemChangeStatus(object sender, RoutedEventArgs e)
        {
            var command = ((MenuItem)sender).Command;
            if (command.CanExecute(null))
            {
                command.Execute(true);
                btnReloadReceiptUI.Command.Execute(null);
            }
        }

        private void menuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            var command = ((MenuItem)sender).Command;
            if (command.CanExecute(null))
            {
                int id;
                if (Int32.TryParse(tbSelectedId.Text, out id))
                {
                    try
                    {
                        command.Execute(id);

                        EditOrderWindow w = new EditOrderWindow();
                        w.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Không thể thực hiện thao tác này", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void menuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận xóa đơn đặt hàng?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((MenuItem)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                try
                {
                    command.Execute(true);

                    MessageBox.Show("Xóa thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Không thể thực hiện thao tác này", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditOrderWindow w = new EditOrderWindow();
            w.ShowDialog();
        }

        private void btnAddReceipt_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận nhập hàng mới?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(true);
                MessageBox.Show("Nhập hàng thành công!");
            }
        }

    }
}
