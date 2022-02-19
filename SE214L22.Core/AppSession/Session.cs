using SE214L22.Data.Entity.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SE214L22.Core.AppSession
{
    public static class Session
    {
        private static User _currentUser;
        private static bool _isMasterAdmin;

        public static User CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;

                throw new Exception("Người dùng phải đăng nhập trước tiên!");
            }
        }
        public static bool IsMasterAdmin
        {
            get
            {
                if (_currentUser == null)
                    return false;
                return _isMasterAdmin;
            }
        }

        public static bool IsLoggedIn()
        {
            return _currentUser != null ? true : false;
        }

        public static void SetSessionUser(User user)
        {
            _currentUser = user;
        }

        public static void SetIsMasterAdmin(bool value)
        {
            _isMasterAdmin = value;
        }

        public static string HashPassword(string password)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytPassword = uEncode.GetBytes(password);
            SHA512Managed sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(bytPassword);
            return Convert.ToBase64String(hash);
        }

        public static string GetNewPassword()
        {
            var random = new Random();
            var newPassword = "";

            // password have at least 1 character and 1 digit
            newPassword += random.Next(0, 9).ToString();
            newPassword += (char)random.Next(97, 122);

            // random the remaining
            for (int i = 0; i < 4; i++)
            {
                var choice = random.Next(0, 2);
                if (choice == 0)
                    newPassword += random.Next(0, 10).ToString();
                else
                    newPassword += (char)random.Next(97, 122);
            }
            return newPassword;
        }

        public static bool ComparePassword(string candidatePassword, string userPassword)
        {
            var hashedCandidatePassword = HashPassword(candidatePassword);

            if (hashedCandidatePassword == userPassword)
                return true;
            return false;
        }
    }
}
