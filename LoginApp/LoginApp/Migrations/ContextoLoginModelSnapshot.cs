﻿// <auto-generated />
using LoginApp.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginApp.Migrations
{
    [DbContext(typeof(ContextoLogin))]
    partial class ContextoLoginModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoginApp.Infraestructura.Modelos.IniciosDeSesion", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccesToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("IniciosDeSesion");
                });
#pragma warning restore 612, 618
        }
    }
}
