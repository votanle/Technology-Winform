using SE214L22.Core.ViewModels.Products;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Helpers;
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
    /// Interaction logic for ItemManagerScreen.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        public ProductUserControl()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void AdvancedSearch_Expanded(object sender, RoutedEventArgs e)
        {
            ListProduct.Height = 350;
        }

        private void AdvancedSearch_Collapsed(object sender, RoutedEventArgs e)
        {
            ListProduct.Height = 500;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var command = ((Button)sender).Command;

            if (command.CanExecute(null))
            {
                command.Execute(true);
                new AddProductWindow().ShowDialog();
            }
        }


        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var command = ((Button)sender).Command;

            if (command.CanExecute(null))
            {
                command.Execute(true);
                new EditProductWindow().ShowDialog();
            }
        }

        private void btnHideProduct_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận xóa sản phẩm?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
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
        private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var listBox = sender as ListBox;
            ScrollViewer scrollviewer = Helper.FindVisualChildren<ScrollViewer>(listBox).FirstOrDefault();
            if (e.Delta > 0)
                scrollviewer.LineLeft();
            else
                scrollviewer.LineRight();
            e.Handled = true;

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            List<Manufacturer> MySelectedManufacturer = new List<Manufacturer>();

            foreach (Manufacturer item in myListManufacturer.SelectedItems)
            {
                MySelectedManufacturer.Add(item);
            }
            var DataContext = (ProductViewModel)this.DataContext;
            DataContext.FilterManufacturer = MySelectedManufacturer;

            List<Category> MySelectedCategory = new List<Category>();

            foreach (Category item in myListCategory.SelectedItems)
            {
                MySelectedCategory.Add(item);
            }
            DataContext.FilterCategory = MySelectedCategory;
        }
    }
}
