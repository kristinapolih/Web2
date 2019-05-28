using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Stanica> Stanicas { get; set; }
        public DbSet<Stavka> Stavkas { get; set; }
        public DbSet<RedVoznje> RedVoznjes { get; set; }
        public DbSet<Linija> Linijas { get; set; }
        public DbSet<Korisnik> Korisniks { get; set; }
        public DbSet<Koordinate> Koordinates { get; set; }
        public DbSet<Koeficijent> Koeficijents { get; set; }
        public DbSet<Karta> Kartas { get; set; }
        public DbSet<CenovnikStavka> CenovnikStavkas { get; set; }
        public DbSet<Cenovnik> Cenovniks { get; set; }
        public DbSet<Adresa> Adresas { get; set; }
    }
}