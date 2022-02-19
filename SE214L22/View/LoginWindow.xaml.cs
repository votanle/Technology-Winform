using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainWindow _mainWindow;

        public LoginWindow()
        {
            InitializeComponent();
            _mainWindow = new MainWindow();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Xác nhận thoát?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                this.Close();
                Application.Current.Shutdown();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var command = ((Button)sender).Command;

            if (command.CanExecute(null))
            {
                try
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait; // set the cursor to loading spinner
                    command.Execute(true);
                    _mainWindow.Show();
                    btnAfterLogin.Command.Execute(null);
                    this.Close();
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow; // set the cursor back to arrow
                }
            }
        }

        private void pwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pwdTb.Text = pwdBox.Password;
        }

        private void tbLoginInfo_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.Key == Key.Enter)
           {
                var command = btnLogin.Command;

                if (command.CanExecute(null))
                {
                    try
                    {
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait; // set the cursor to loading spinner
                        command.Execute(true);
                        _mainWindow.Show();
                        btnAfterLogin.Command.Execute(null);
                        this.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow; // set the cursor back to arrow
                    }
                }
           }
        }
    }
}
