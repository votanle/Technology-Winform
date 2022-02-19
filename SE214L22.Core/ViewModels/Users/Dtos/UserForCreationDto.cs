using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Core.ViewModels.Users.Dtos
{
    public class UserForCreationDto : BaseDto
    {
        private int? _id;
        private string _name;
        private string _role;
        private DateTime _dob;
        private string _phoneNumber;
        private string _address;
        private DateTime _creationTime;
        private string _email;
        private string _photo;

        public int? Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string Role { get => _role; set { _role = value; OnPropertyChanged(); } }
        public DateTime Dob { get => _dob; set { _dob = value; OnPropertyChanged(); } }
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(); } }
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }
        public DateTime CreationTime { get => _creationTime; set { _creationTime = value; OnPropertyChanged(); } }
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public string Photo { get => _photo; set { _photo = value; OnPropertyChanged(); } }
    }
}
