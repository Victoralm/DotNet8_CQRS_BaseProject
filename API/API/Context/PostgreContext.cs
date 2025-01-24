using API.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace API.Context;

public class PostgreContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public PostgreContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //connect to postgres with connection string from app settings
        var ops = options.UseNpgsql(Configuration.GetConnectionString("PostgreSql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure default schema
        modelBuilder.HasDefaultSchema("dev");
        base.OnModelCreating(modelBuilder);
    }

    public IDbConnection DapperConnection()
    {
        var connectionString = Configuration.GetConnectionString("PostgreSql");
        return new NpgsqlConnection(connectionString);
    }

    // DbSets
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
