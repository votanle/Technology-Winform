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
    /// Interaction logic for NhanVienUserControl.xaml
    /// </summary>
    public partial class StaffUserControl : UserControl
    {
        public StaffUserControl()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var command = ((Button)sender).Command;

            if (command.CanExecute(null))
            {
                command.Execute(true);
                new AddStaffWindow().ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Xác nhận xóa nhân viên này?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                var command = ((Button)sender).Command;

                if (result == MessageBoxResult.OK && command.CanExecute(null))
                {
                    //btnCheckPermission.Command.Execute(null);
                    command.Execute(true);
                }
                else if (result != MessageBoxResult.OK && command.CanExecute(null))
                {
                    command.Execute(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void btnGrantNewPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Xác nhận cấp mật khẩu mới cho nhân viên?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                var command = ((Button)sender).Command;

                if (result == MessageBoxResult.OK && command.CanExecute(null))
                {
                   // btnCheckPermission.Command.Execute(null);
                    command.Execute(true);
                }
                else if (result != MessageBoxResult.OK && command.CanExecute(null))
                {
                    command.Execute(false);
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var command = ((Button)sender).Command;

                if (command.CanExecute(null))
                {
                    //btnCheckPermission.Command.Execute(null);
                    command.Execute(true);
                    new UpdateInfoStaffWindow().ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
