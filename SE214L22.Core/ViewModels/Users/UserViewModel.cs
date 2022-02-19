using Microsoft.Win32;
using SE214L22.Core.AppSession;
using SE214L22.Core.Services.AppUser;
using SE214L22.Core.ViewModels.Users.Dtos;
using SE214L22.Data.Entity.AppUser;
using SE214L22.Shared.AppConsts;
using SE214L22.Shared.Permissions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SE214L22.Core.ViewModels.Users
{
    public class UserViewModel : BaseViewModel
    {
        // private service fields
        private readonly UserService _userService;
        private readonly RoleService _roleService;

        // private data fields
        private ObservableCollection<User> _users;
        private UserForCreationDto _editingUser;
        private ObservableCollection<string> _roles;
        private User _chosenUser;


        // public data properties
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public UserForCreationDto EditingUser
        {
            get => _editingUser;
            set
            {
                _editingUser = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Roles { 
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged();
            }
        }
        public User ChosenUser
        {
            get => _chosenUser;
            set
            {
                _chosenUser = value;
                if (value != null)
                    _chosenUser.Photo = GetPhotoPath(value.Photo);
                OnPropertyChanged();
            }
        }


        // public command properties
        public ICommand SaveEditingUser { get; set; }
        public ICommand SelectPhoto { get; set; }
        public ICommand DeleteUser { get; set; }
        public ICommand ReloadUsers { get; set; }
        public ICommand GrantNewPassword { get; set; }
        public ICommand PrepareForCreateUser { get; set; }
        public ICommand PrepareForUpdateUser { get; set; }
        public ICommand CheckModificationPermission { get; set; }

        public UserViewModel()
        {
            _userService = new UserService();
            _roleService = new RoleService();

            Users = new ObservableCollection<User>(_userService.GetUsers());
            EditingUser = new UserForCreationDto
            {
                CreationTime = DateTime.Now,
                Dob = new DateTime(2000, 1, 1),
                Photo = GetPhotoPath(DefaultPhotoNames.User)
            };

            Roles = new ObservableCollection<string>(_roleService.GetAllRolesNames());

            SaveEditingUser = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        if (EditingUser.Id == null)
                            _userService.AddUser(EditingUser);
                        else
                            _userService.UpdateUser(EditingUser, ChosenUser);

                        Users = new ObservableCollection<User>(_userService.GetUsers());
                    }
                }
            );

            SelectPhoto = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                    if (fileDialog.ShowDialog() == true)
                    {
                        EditingUser.Photo = fileDialog.FileName;

                    }
                    else
                    {
                        EditingUser.Photo = null;
                    }
                }
            );

            DeleteUser = new RelayCommand<object>
            (
                p => ChosenUser == null ? false : true,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        _userService.DeleteUser(ChosenUser);
                        Users = new ObservableCollection<User>(_userService.GetUsers());
                    }
                       
                }
            );

            GrantNewPassword = new RelayCommand<object>
            (
                p => ChosenUser != null,
                p =>
                {
                    if (p != null && (bool)p == true)
                    {
                        var newUserPassword = _userService.GetNewPassword(ChosenUser.Id);
                        MessageBox.Show($"Mật khẩu mới của nhân viên {ChosenUser.Name} là {newUserPassword} !");
                    }
                }
            );

            PrepareForCreateUser = new RelayCommand<object>
            (
                p => true,
                p =>
                {
                    EditingUser = new UserForCreationDto 
                    { 
                        CreationTime = DateTime.Now, 
                        Dob = new DateTime(2000, 1, 1),
                        Photo = GetPhotoPath(DefaultPhotoNames.User)
                    };
                    if (Roles.Count > 0) EditingUser.Role = Roles.FirstOrDefault();
                }
            );

            PrepareForUpdateUser = new RelayCommand<object>
            (
                p => ChosenUser != null,
                p =>
                {
                    EditingUser = Mapper.Map<UserForCreationDto>(ChosenUser);
                    EditingUser.Photo = GetPhotoPath(EditingUser.Photo != null ? EditingUser.Photo : DefaultPhotoNames.User);
                }
            );

            CheckModificationPermission = new RelayCommand<object>
            (
                p => ChosenUser != null,
                p =>
                {
                    if (!CurrentUser.Role.Permissions.Select(pm => pm.Name).Contains(PermissionsNames.User))
                        throw new Exception("Bạn không có quyền thực hiện thao tác này!");

                    var grantPermission = false;
                    var chosenUserIsMasterAdmin = MasterAdmins.Emails.Contains(ChosenUser.Email);
                    var currentUserIsMasterAdmin = Session.IsMasterAdmin;
                    //// master admin -- current: master admin
                    if (chosenUserIsMasterAdmin && Session.IsMasterAdmin)
                    {
                        if (ChosenUser.Id == CurrentUser.Id)
                            grantPermission = true;
                        else
                            grantPermission = false;
                    }

                    // admin ?
                    if (chosenUserIsMasterAdmin && currentUserIsMasterAdmin)
                    {
                        if (ChosenUser.Id == CurrentUser.Id)
                            grantPermission = true;
                        else
                            grantPermission = false;
                    }
                    else if (!chosenUserIsMasterAdmin && currentUserIsMasterAdmin)
                    {
                        grantPermission = true;
                    }
                    else if (chosenUserIsMasterAdmin && !currentUserIsMasterAdmin)
                    {
                        grantPermission = false;
                    }
                    else if (ChosenUser.Role.Name == RoleNames.Admin)
                    {
                        if (ChosenUser.Id == CurrentUser.Id)
                            grantPermission = true;
                        else
                            grantPermission = false;
                    }
                    else
                    {
                        grantPermission = true;
                    }

                    if (!grantPermission)
                        throw new Exception("Bạn không có quyền thực hiện thao tác lên người dùng này!");

                }
            );
        }

        private string GetPhotoPath(string fileName)
        {
            string destPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string destinationFile = Path.Combine(destPath, "Photos", "Users", fileName);
            return destinationFile;
        }
    }
}
