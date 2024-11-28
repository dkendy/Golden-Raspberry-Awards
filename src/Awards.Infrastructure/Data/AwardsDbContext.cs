using System;
using Awards.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Awards.Infraestructure.Data;

public class AwardsDbContext : DbContext
{
    public AwardsDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Nominate> Nominates { get; set; }
}
