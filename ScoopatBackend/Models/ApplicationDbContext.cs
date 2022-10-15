using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoopatBackend.Models.Farmers;
using ScoopatBackend.Models.Users;

namespace ScoopatBackend.Models;

public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Farmer> Farmers { get; set; }
    public DbSet<Farm> Farms { get; set; }
    public DbSet<CultivationInformation> CultivationInformations { get; set; }
    public DbSet<FarmOwner> FarmOwners { get; set; }
    public DbSet<OwnersFarms> OwnersFarms { get; set; }
    public DbSet<FarmersFarm> FarmersFarms { get; set; }
    public DbSet<Workers> Workers { get; set; }


}