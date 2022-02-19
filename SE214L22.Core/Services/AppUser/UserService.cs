using SE214L22.Core.AppSession;
using SE214L22.Core.ViewModels.Users.Dtos;
using SE214L22.Data.Entity.AppUser;
using SE214L22.Data.Repository;
using SE214L22.Shared.AppConsts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.Services.AppUser
{
    public class UserService : BaseService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
        }

        public User GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User Login(LoginDto loginDto)
        {
            // get user with email
            var user = _userRepository.GetUserByEmail(loginDto.Email);

            // compare password?
            if (user == null || !Session.ComparePassword(loginDto.Password, user.Password))
            throw new ArgumentException("Email hoặc mật khẩu không chính xác");

            // ok?
            return user;
        }

        public User AddUser(UserForCreationDto userForCreation)
        {
            // copy to save photo
            string newName = GetImageName();
            string desFile = GetFullPath(newName);
            try
            {
                File.Copy(userForCreation.Photo, desFile, true);
            }
            catch
            {
                System.Windows.MessageBox.Show("Đã xảy ra lỗi khi lưu file!");
                return null;
            }

            // save user
            var newUser = Mapper.Map<User>(userForCreation);
            newUser.Photo = newName;
            var role = _roleRepository.GetRoleByName(userForCreation.Role);
            newUser.RoleId = role.Id;

            return _userRepository.Create(newUser);
        }

        public void UpdateUser(UserForCreationDto userForUpdate, User currentUser)
        {

            var user = Mapper.Map<User>(userForUpdate);
            // check if there is a photo change?
            if (userForUpdate.Photo != currentUser.Photo)
            {
                // delete old photo
                var oldPhotoName = _userRepository.GetUserPhotoById((int)userForUpdate.Id);
                if (oldPhotoName != DefaultPhotoNames.User)
                {
                    try
                    {
                        File.Delete(GetFullPath(oldPhotoName));
                    }
                    catch
                    {

                    }
                }
                
                string newName = GetImageName();
                // copy to save as new photo
                string desFile = GetFullPath(newName);
                try
                {
                    File.Copy(userForUpdate.Photo, desFile, true);
                    user.Photo = newName;
                }
                catch
                {
                    System.Windows.MessageBox.Show("Đã xảy ra lỗi khi lưu file!");
                    return;
                }
            } else
            {
                user.Photo = _userRepository.GetUserPhotoById((int)userForUpdate.Id);
            }

            // save user
            var role = _roleRepository.GetRoleByName(userForUpdate.Role);
            user.Id = (int)userForUpdate.Id;
            user.RoleId = role.Id;
            _userRepository.Update(user);
        }

        public bool DeleteUser(User user)
        {
            var oldPhotoName = _userRepository.GetUserPhotoById(user.Id);
            if (oldPhotoName != DefaultPhotoNames.Product)
            {
                try
                {
                    File.Delete(GetFullPath(oldPhotoName));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine(e.Message);
                }
            }
            return _userRepository.Delete(user.Id);
        }

        public bool EmailHasBeenTaken(string email)
        {
            return _userRepository.GetUserByEmail(email) != null;
        }

        public string GetNewPassword(int userId)
        {
            // 1. Generate new password
            var newPassword = Session.GetNewPassword();

            // 2. Hash new password just generated
            var hashedPassword = Session.HashPassword(newPassword);

            // 3. save new user's password in to DB
            _userRepository.UpdateUserPassword(userId, hashedPassword);

            return newPassword;

        }

        public void UpdateUserPassword(UserForPasswordUpdateDto userForPasswordUpdate)
        {
            var hashedPassword = Session.HashPassword(userForPasswordUpdate.Password);
            _userRepository.UpdateUserPassword(userForPasswordUpdate.Id, hashedPassword);
        }

        private string GetFullPath(string fileName)
        {
            string destPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string destinationFile = Path.Combine(destPath, "Photos", "Users", fileName);
            return destinationFile;
        }
        
        private string GetImageName()
        {
            var now = DateTime.Now.ToString("HHmmss_ddMMyyyy");
            return $"user_{now}.jpg";
        }   
    }
}
