using Microsoft.EntityFrameworkCore;
using ORMDatabaseSystem.Models;

namespace ORMDatabaseSystem.Db
{
    public class ProduktDb : DbContext
    {
        // Nutné odkázat se na složku Models (using ORMDatabseSystem.Models), pojmenovávám plurálem
        public DbSet<Produkt> Produkty { get; set; }

        // konfigurujeme cestu a chování naší databáze
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Produkty.db"); // definujeme náš DB soubor typu sqlite3
        }

        // při tvorbě souboru s DB nám to vytvoří implicitní položky
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produkt>().HasData(
                new Produkt { Id = 1, Nazev = "Kapr s klackem", Cena = 120 },
                new Produkt { Id = 2, Nazev = "Marek s kaprem", Cena = 200 },
                new Produkt { Id = 3, Nazev = "Kapr s Markem", Cena = 500 }
                );
        }
    }
}
