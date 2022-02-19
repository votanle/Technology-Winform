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
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        public UpdatePasswordWindow()
        {
            InitializeComponent();
        }

        private void currPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tbCurrentPassword.Text = pbCurrentPassword.Password;
        }

        private void newPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tbNewPassword.Text = pbNewPassword.Password;
        }

        private void confirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tbPasswordConfirm.Text = pbPasswordConfirm.Password;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận đổi mật khẩu?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            var command = ((Button)sender).Command;

            if (result == MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(true);
                this.Close();
                MessageBox.Show("Đổi mật khẩu thành công!");
            }
            else if (result != MessageBoxResult.OK && command.CanExecute(null))
            {
                command.Execute(false);
            }
        }
    }
}
