using ComicBookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicBookAPI.Data {
    public class ApiContext : DbContext {
        public DbSet<ComicBook> ComicBooks { set; get; }
        public ApiContext (DbContextOptions<ApiContext> options) : base (options) { }
    }
}