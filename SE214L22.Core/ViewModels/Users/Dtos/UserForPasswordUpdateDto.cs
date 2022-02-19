using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Users.Dtos
{
    public class UserForPasswordUpdateDto : BaseDto
    {
        private string _currentPasseword;
        private string _password;
        private string _passwordConfirm;

        public int Id { get; set; }
        public string CurrentPassword { get => _currentPasseword; set { _currentPasseword = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string PasswordConfirm { get => _passwordConfirm; set { _passwordConfirm = value; OnPropertyChanged(); } }
    }
}
