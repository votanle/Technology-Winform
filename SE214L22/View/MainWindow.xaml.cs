using SE214L22.Core.ViewModels.Products;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _mouseDown;
        private double x;
        private double y;

        private HomeScreen _homeScreen;
        private SellUserControl _sellScreen;
        private ProductUserControl _itemManagementScreen;
        private ManagementOrderUserControl _orderManagementScreen;
        private ListWarrantyOrderUserControl _warrantyOrderScreen;
        private StaffUserControl _userScreen;
        private ReportManagementUserControl _reportScreen;
        private SettingUserControl _settingScreen;
        private CustomerUserControl _customerScreen;

        public MainWindow()
        {
            InitializeComponent();

            _mouseDown = false;

            _homeScreen = new HomeScreen();
            _sellScreen = new SellUserControl();
            _itemManagementScreen = new ProductUserControl();
            _orderManagementScreen = new ManagementOrderUserControl();
            _warrantyOrderScreen = new ListWarrantyOrderUserControl();
            _userScreen = new StaffUserControl();
            _reportScreen = new ReportManagementUserControl();
            _settingScreen = new SettingUserControl();
            _customerScreen = new CustomerUserControl();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainControl.Content = _homeScreen;
            //ListViewMenu.SelectedIndex = 0;
        }
        private void changeColorUserControlItem(ListViewItem nameUserControl)
        {
            ItemHome.Opacity = 0.8;
            ItemSell.Opacity = 0.8;
            ItemProduct.Opacity = 0.8;
            ItemImportProduct.Opacity = 0.8;
            ItemWarranty.Opacity = 0.8;
            ItemStaff.Opacity = 0.8;
            ItemReport.Opacity = 0.8;
            ItemSetting.Opacity = 0.8;
            ItemCustomer.Opacity = 0.8;
            nameUserControl.Opacity = 1;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    lbTitle.Content = "TRANG CHỦ";
                    mainControl.Content = _homeScreen;
                    changeColorUserControlItem(ItemHome);
                    break;
                case "ItemSell":
                    this.lbTitle.Content = "BÁN HÀNG";
                    mainControl.Content = _sellScreen;
                    changeColorUserControlItem(ItemSell);
                    break;
                case "ItemProduct":
                    this.lbTitle.Content = "QUẢN LÝ SẢN PHẨM";
                    mainControl.Content = _itemManagementScreen;
                    changeColorUserControlItem(ItemProduct);
                    break;
                case "ItemImportProduct":
                    this.lbTitle.Content = "QUẢN LÝ ĐƠN ĐẶT HÀNG";
                    mainControl.Content = _orderManagementScreen;
                    changeColorUserControlItem(ItemImportProduct);
                    break;
                case "ItemWarranty":
                    this.lbTitle.Content = "BẢO HÀNH SẢN PHẨM";
                    mainControl.Content = _warrantyOrderScreen;
                    changeColorUserControlItem(ItemWarranty);
                    break;
                case "ItemStaff":
                    this.lbTitle.Content = "QUẢN LÝ NHÂN VIÊN";
                    changeColorUserControlItem(ItemStaff);
                    mainControl.Content = _userScreen;
                    break;
                case "ItemReport":
                    this.lbTitle.Content = "BÁO CÁO THỐNG KÊ";
                    mainControl.Content = _reportScreen;
                    changeColorUserControlItem(ItemReport);
                    break;
                case "ItemSetting":
                    this.lbTitle.Content = "THAY ĐỔI CÁC THAM SỐ";
                    mainControl.Content = _settingScreen;
                    changeColorUserControlItem(ItemSetting);
                    break;
                case "ItemCustomer":
                    this.lbTitle.Content = "QUẢN LÝ KHÁCH HÀNG";
                    mainControl.Content = _customerScreen;
                    changeColorUserControlItem(ItemCustomer);
                    break;
            }
        }

        private void lbTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown == true)
            {
                var position = e.GetPosition(this);
                this.Left += position.X - x;
                this.Top += position.Y - y;
            }
        }

        private void lbTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDown = true;
            var position = e.GetPosition(this);
            this.x = position.X;
            this.y = position.Y;
        }

        private void lbTitle_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            (new LoginWindow()).Show();
            this.Close();
        }

        private void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            new UpdatePasswordWindow().ShowDialog();
        }
    }
}
