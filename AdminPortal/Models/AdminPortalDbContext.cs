using Microsoft.EntityFrameworkCore;

namespace AdminPortal.Models;

public class AdminPortalDbContext(DbContextOptions<AdminPortalDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
}