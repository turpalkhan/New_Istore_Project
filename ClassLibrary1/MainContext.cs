using Delete.ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;

namespace Delete.ClassLibrary1;

public class MainContext : DbContext
{
    public DbSet<News> News { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<ArhivPhone> ArhivPhones { get; set; }
    public DbSet<Zakaz> Zakazs { get; set; }
    public DbSet<Pokupka> Pokupkas { get; set; }

public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
        Database.EnsureCreated();
        //Database.EnsureDeleted();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new User { Id = 1, Role = Role.Адининстратор.ToString(), Name = "Турпал", Surname = "Мамакаев", Login = "turpal", Email = "turpalkhna11@gmail.com", Password = "111" },
            new User { Id = 2, Role = Role.Адининстратор.ToString(), Name = "Алихан", Surname = "Исхаджиев", Login = "alikhan", Email = "alikhan@gmail.com", Password = "222" }
        });
    }
}