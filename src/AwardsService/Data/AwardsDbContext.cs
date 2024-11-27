using System;
using AwardsService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwardsService.Data;

public class AwardsDbContext : DbContext
{
    public AwardsDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Nominate> Nominates { get; set; }
}
