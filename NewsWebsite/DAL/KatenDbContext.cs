using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.Models;
using System.Collections.Generic;

namespace NewsWebsite.DAL
{
    public class KatenDbContext : IdentityDbContext
    {
        public KatenDbContext(DbContextOptions<KatenDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<InformationImage> InformationImages { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
      
    }
}
