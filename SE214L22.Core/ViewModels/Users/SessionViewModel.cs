using SE214L22.Core.Services.AppProduct;
using SE214L22.Core.Services.AppUser;
using SE214L22.Core.AppSession;
using SE214L22.Core.ViewModels.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SE214L22.Data.Entity.AppUser;
using SE214L22.Shared.Permissions;

namespace SE214L22.Core.ViewModels.Users
{
    public class SessionViewModel : BaseViewModel
    {
        // service
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        // data field
        private LoginDto _loginDto;
        private UserForPasswordUpdateDto _userForPasswordUpdate;
        private ObservableCollection<Permission> _userPerrmissions;

        // data property
        public LoginDto LoginDto { get => _loginDto; set { _loginDto = value; OnPropertyChanged(); } }

        public string UserName
        {
            get
            {
                if (!Session.IsLoggedIn())
                    return null;
                return Session.CurrentUser.Name;
            }
        }

        public UserForPasswordUpdateDto UserForPasswordUpdate
        {
            get => _userForPasswordUpdate;
            set
            {
                _userForPasswordUpdate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Permission> UserPerrmissions
        {
            get => _userPerrmissions;
            set
            {
                _userPerrmissions = value;
                OnPropertyChanged();
            }
        }


        // command
        public ICommand Login { get; set; }
        public ICommand Logout { get; set; }
        public ICommand ReloadUsername { get; set; }
        public ICommand UpdatePassword { get; set; }

        public SessionViewModel()
        {
            _userService = new UserService();
            _roleService = new RoleService();

            LoginDto = new LoginDto { Email = "votanle@gmail.com", Password = "" };
            UserForPasswordUpdate = new UserForPasswordUpdateDto();
            UserPerrmissions = new ObservableCollection<Permission>();


            Login = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        var authenticatedUser = _userService.Login(LoginDto);

                        // setup session and permissions info
                        Session.SetSessionUser(authenticatedUser);
                        UserPerrmissions = new ObservableCollection<Permission>(_roleService.GetRolePermissions(authenticatedUser.RoleId));

                        // check if this is a master admin?
                        if (MasterAdmins.Emails.Contains(authenticatedUser.Email))
                            Session.SetIsMasterAdmin(true);
                        else
                            Session.SetIsMasterAdmin(false);

                        // reset input
                        LoginDto.Password = "";
                    }
                } 
            );

            Logout = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        Session.SetSessionUser(null);
                        UserPerrmissions = new ObservableCollection<Permission>();
                        Session.SetIsMasterAdmin(false);
                    }
                }
            );

            ReloadUsername = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    var x = UserName;
                    OnPropertyChanged(nameof(UserName));
                }
            );

            UpdatePassword = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p)
                    {
                        UserForPasswordUpdate.Id = CurrentUser.Id;
                        _userService.UpdateUserPassword(UserForPasswordUpdate);
                    }
                }
            );
        }
    }
}
