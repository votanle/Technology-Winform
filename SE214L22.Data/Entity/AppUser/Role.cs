using System.Collections.Generic;

namespace SE214L22.Data.Entity.AppUser
{
    public class Role : AppEntity
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
