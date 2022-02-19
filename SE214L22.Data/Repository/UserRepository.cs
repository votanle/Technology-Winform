using SE214L22.Data.Entity.AppUser;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE214L22.Data.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public IEnumerable<User> GetAllUsers()
        {
            using (var ctx = new AppDbContext())
            {
                var users = ctx.Users
                    .FilterDeleted()
                    .Include(u => u.Role)
                    .ToList();
                return users;
            }
        }

        public User GetUserByEmail(string email)
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.Users
                    .FilterDeleted()
                    .Where(u => u.Email == email)
                    .Include(u => u.Role)
                    .Include(u => u.Role.Permissions)
                    .FirstOrDefault();
            }
        }

        public void UpdateUserPassword(int userId, string hashedPassword)
        {
            using (var ctx = new AppDbContext())
            {
                var user = ctx.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.Password = hashedPassword;
                    ctx.SaveChanges();
                }
                else
                    throw new Exception("Người dùng này không tồn tại!");
            }
        }

        public int CountUsers()
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.Users.Count();
            }
        }

        public string GetUserPhotoById(int id)
        {
            using (var ctx = new AppDbContext())
            {
                return ctx.Users.Where(x => x.Id == id).Select(x => x.Photo).FirstOrDefault();
            }
        }
    }

    static class Extension
    {
        public static IQueryable<User> FilterDeleted(this IQueryable<User> query)
        {
            return query.Where(u => u.IsDeleted != true);
        }
    }
}
