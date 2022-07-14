using LoginApp.Infraestructura.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace LoginApp.Infraestructura
{
    public class ContextoLogin : DbContext
    {
        public ContextoLogin(DbContextOptions<ContextoLogin> options) :base(options)
        {
            if ((Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                this.Database.Migrate();
            }
            else
            {
                throw new Exception("No se ha creado la base de datos");
            }
        }

        public DbSet<CuentasCorreo> CuentasCorreo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuentasCorreo>().Property(s => s.Email).IsUnicode(false);
            modelBuilder.Entity<CuentasCorreo>().Property(s => s.AccesToken).IsUnicode(false);
            modelBuilder.Entity<CuentasCorreo>().Property(s => s.RefreshToken).IsUnicode(false);
            modelBuilder.Entity<CuentasCorreo>().Property(s => s.Scope).IsUnicode(false);
        }
    }
}
