using SE214L22.Data.Entity.AppUser;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SE214L22.Data.Repository
{
    public class RoleRepository : BaseRepository<Role>
    {
        public override Role Create(Role role)
        {
            using (var ctx = new AppDbContext())
            {
                var roleCreate = ctx.Roles.Add(role);
                ctx.SaveChanges();
                return roleCreate;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            using (var ctx = new AppDbContext())
            {
                var roles = ctx.Roles.ToList();
                return roles;
            }
        }

        public IEnumerable<string> GetAllRolesName()
        {
            using (var ctx = new AppDbContext())
            {
                var roles = ctx.Roles.ToList();
                var rolesNames = roles.Select(r => r.Name);
                return rolesNames;
            }
        }

        public IEnumerable<Permission> GetRolePermissions(int roleId)
        {
            using (var ctx = new AppDbContext())
            {
                var role = ctx.Roles
                    .Where(r => r.Id == roleId)
                    .Include(r => r.Permissions)
                    .FirstOrDefault();
                return role.Permissions;
            }
        }

        public Role GetRoleByName(string name)
        {
            using (var ctx = new AppDbContext())
            {
                var role = ctx.Roles.Where(r => r.Name == name).FirstOrDefault();
                return role;
            }
        }
    }
}
