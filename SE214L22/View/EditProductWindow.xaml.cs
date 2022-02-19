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

namespace SE214L22.View
{
    /// <summary>
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        private bool isLoaded;
        public EditProductWindow()
        {
            isLoaded = true;
            InitializeComponent();
        }

        private void ReTurnRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isLoaded)
            {
                isLoaded = false;
                return;
            }
            tbCheckReturnRateChange.Text = "changed";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận sửa sản phẩm?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(true);
                if (btnAfterEdit.Command.CanExecute(null) == true) btnAfterEdit.Command.Execute(null);
                this.Close();
            }
            else if (result != MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(false);
            }
        }

        private void cbStatus_Loaded(object sender, RoutedEventArgs e)
        {
            cbStatus.Items.Clear();
            cbStatus.Items.Add("Đang kinh doanh");
            cbStatus.Items.Add("Ngừng kinh doanh");
        }
    }
}
