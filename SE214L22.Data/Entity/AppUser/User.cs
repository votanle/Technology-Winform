using System;

namespace SE214L22.Data.Entity.AppUser
{
    public class User : AppEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }

        public Role Role { get; set; }

        public User()
        {
            IsDeleted = false;
        }
    }
}
