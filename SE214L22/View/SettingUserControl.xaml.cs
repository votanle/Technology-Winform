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
    /// Interaction logic for SettingUserControl.xaml
    /// </summary>
    public partial class SettingUserControl : UserControl
    {
        public SettingUserControl()
        {
            InitializeComponent();
        }
        //catergory
        private void btnAddCategoryProduct_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryProductWindow w = new AddCategoryProductWindow();
            w.ShowDialog();
        }

        private void btnEditCategory_Click(object sender, RoutedEventArgs e)
        {
            EditCategoryProductWindow w = new EditCategoryProductWindow();
            w.ShowDialog();
        }

        private void btnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận xóa loại sản phẩm?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(true);
                if (btnAfterCategory.Command.CanExecute(null) == true) btnAfterCategory.Command.Execute(null);
            }
            else if (result != MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(false);
            }
        }

        //manufacture
        private void btnAddManufacture_Click(object sender, RoutedEventArgs e)
        {
            AddManufactureWindow w = new AddManufactureWindow();
            w.ShowDialog();
        }

        private void btnDeleteManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận xóa nhà sản xuất?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(true);
                if (btnAfterManufacturer.Command.CanExecute(null) == true) btnAfterManufacturer.Command.Execute(null);
            }
            else if (result != MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(false);
            }
        }
        //provider
        private void btnAddProvider_Click(object sender, RoutedEventArgs e)
        {
            AddProviderWindow w = new AddProviderWindow();
            w.ShowDialog();
        }
        //customerLevel
        private void btnEditCustomerLevel_Click(object sender, RoutedEventArgs e)
        {
            EditCustomerLevel w = new EditCustomerLevel();
            w.ShowDialog();
        }

    }
}
