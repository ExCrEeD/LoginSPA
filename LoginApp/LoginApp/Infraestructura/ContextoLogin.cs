using LoginApp.Infraestructura.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.Infraestructura
{
    public class ContextoLogin : DbContext
    {
        public ContextoLogin(DbContextOptions<ContextoLogin> options) :base(options)
        {

        }

        public DbSet<IniciosDeSesion> IniciosDeSesion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IniciosDeSesion>().Property(s => s.Email).IsUnicode(false);
            modelBuilder.Entity<IniciosDeSesion>().Property(s => s.AccesToken).IsUnicode(false);
            modelBuilder.Entity<IniciosDeSesion>().Property(s => s.RefreshToken).IsUnicode(false);
            modelBuilder.Entity<IniciosDeSesion>().Property(s => s.Scope).IsUnicode(false);
        }
    }
}
