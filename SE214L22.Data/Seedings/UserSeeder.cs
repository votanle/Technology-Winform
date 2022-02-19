using SE214L22.Data.Entity.AppUser;
using SE214L22.Shared.AppConsts;
using SE214L22.Shared.Permissions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SE214L22.Data.Seedings
{
    public class UserSeeder 
    {

        public static void Seed(AppDbContext context)
        {
            // permissions
            var salePermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.Sale });
            var warrantyPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.Warranty });
            var productPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.Product });
            var orderPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.Order });
            var reportPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.Report });
            var userPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.User });
            var customerPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.Customer });
            var basicSettingPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.BasicSetting });
            var fullSettingPermission = context.Permissions.Add(new Permission { Id = 1, Name = PermissionsNames.FullSetting });

            context.SaveChanges();

            // roles
            var quanTriVienRole = context.Roles.Add(new Role
            {
                Name = RoleNames.Admin,
                Permissions = new List<Permission>()
                {
                    salePermission,
                    warrantyPermission,
                    productPermission,
                    orderPermission,
                    basicSettingPermission,
                    fullSettingPermission,
                    reportPermission,
                    userPermission,
                    customerPermission
                }
            });
            var thuKhoRole = context.Roles.Add(new Role
            {
                Name = RoleNames.Warehouseman,
                Permissions = new List<Permission>()
                {
                    productPermission,
                    orderPermission,
                    basicSettingPermission
                }
            });
            var nhanVienBanHangRole = context.Roles.Add(new Role
            {
                Name = RoleNames.SalesPerson,
                Permissions = new List<Permission>()
                {
                    salePermission,
                    warrantyPermission,
                    customerPermission
                }
            });
            context.SaveChanges();


            // users
            var quantrivienUser = context.Users.Add(new User
            {
                Id = 1,
                Name = "Lê Anh Tuấn",
                Email = "letgo237@gmail.com",
                Password = HashPassword("test1234"),
                Address = "Thành phố Hồ Chí Minh",
                RoleId = quanTriVienRole.Id,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Dob = new DateTime(2000, 1, 1),
                PhoneNumber = "0369636841",
                Photo = DefaultPhotoNames.User
            });
            var nhanVien1User = context.Users.Add(new User
            {
                Id = 2,
                Name = "Nguyễn Xuân Tú",
                Email = "nguyenxuantu@gmail.com",
                Password = HashPassword("test1234"),
                Address = "Thành phố Hồ Chí Minh",
                RoleId = thuKhoRole.Id,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Dob = new DateTime(2000, 1, 1),
                PhoneNumber = "0378678408",
                Photo = DefaultPhotoNames.User
            });
            var nhanVien2User = context.Users.Add(new User
            {
                Id = 3,
                Name = "Nguyễn Thanh Tuấn",
                Email = "nguyenthanhtuan@gmail.com",
                Password = HashPassword("test1234"),
                Address = "Thành phố Hồ Chí Minh",
                RoleId = nhanVienBanHangRole.Id,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Dob = new DateTime(2000, 1, 1),
                PhoneNumber = "04378432980",
                Photo = DefaultPhotoNames.User
            });
            var nhanVien3User = context.Users.Add(new User
            {
                Id = 4,
                Name = "Lê Xuân Tùng",
                Email = "lexuantung@gmail.com",
                Password = HashPassword("test1234"),
                Address = "Thành phố Hồ Chí Minh",
                RoleId = nhanVienBanHangRole.Id,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Dob = new DateTime(2000, 1, 1),
                PhoneNumber = "01224578220",
                Photo = DefaultPhotoNames.User
            });
            var nhanVien4User = context.Users.Add(new User
            {
                Id = 4,
                Name = "Trần Duy Khánh",
                Email = "tranduykhanh@gmail.com",
                Password = HashPassword("test1234"),
                Address = "Thành phố Hồ Chí Minh",
                RoleId = nhanVienBanHangRole.Id,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Dob = new DateTime(2000, 1, 1),
                PhoneNumber = "01224578220",
                Photo = DefaultPhotoNames.User
            });
            var nhanVien5User = context.Users.Add(new User
            {
                Id = 4,
                Name = "Dương Thành Vương",
                Email = "duongthanhvuong@gmail.com",
                Password = HashPassword("test1234"),
                Address = "Thành phố Hồ Chí Minh",
                RoleId = thuKhoRole.Id,
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Dob = new DateTime(2000, 1, 1),
                PhoneNumber = "01224578220",
                Photo = DefaultPhotoNames.User
            });
            context.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytPassword = uEncode.GetBytes(password);
            SHA512Managed sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(bytPassword);
            return Convert.ToBase64String(hash);
        }
    }
}
