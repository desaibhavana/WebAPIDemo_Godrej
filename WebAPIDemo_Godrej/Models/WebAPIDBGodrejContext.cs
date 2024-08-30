using Microsoft.EntityFrameworkCore;

namespace WebAPIDemo_Godrej.Models
{
    public class WebAPIDBGodrejContext : DbContext
    {
        public WebAPIDBGodrejContext(DbContextOptions<WebAPIDBGodrejContext> options) : base(options)
        { }


        public DbSet<Product> Products { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
    }
}

