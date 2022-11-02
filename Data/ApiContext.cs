using ComicBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicBookAPI.Data {
    public class ApiContext : DbContext {
        private DbSet<User> users;
        public DbSet<ComicBook> ComicBooks { set; get; }
        public DbSet<User> Users {
            get { return users; }
            set {
                new User { Username = "admin", Password = "admin" };
            }
        }
        public ApiContext (DbContextOptions<ApiContext> options) : base (options) { }
    }
}