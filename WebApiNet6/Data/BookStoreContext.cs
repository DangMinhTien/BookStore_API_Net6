using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiNet6.Data
{
    public class BookStoreContext : IdentityDbContext<AppUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> opt) : base(opt)
        {
            
        }
        #region DbSet
        public DbSet<Book> Books { get; set; }
        #endregion
    }
}
