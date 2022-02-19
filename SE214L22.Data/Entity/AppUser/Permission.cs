using System.Collections.Generic;

namespace SE214L22.Data.Entity.AppUser
{
    public class Permission : AppEntity
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
