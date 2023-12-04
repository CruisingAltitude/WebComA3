using Microsoft.EntityFrameworkCore;
using AdminPortal.Models;

namespace AdminPortal.Models;

public class AdminPortalDbContext(DbContextOptions<AdminPortalDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Article> Articles { get; set; }
}