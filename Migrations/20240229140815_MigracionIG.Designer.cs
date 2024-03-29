﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using T_Reservation.Models;

#nullable disable

namespace T_Reservation.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240229140815_MigracionIG")]
    partial class MigracionIG
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("T_Reservation.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("Dui")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Passaword")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RestauranteId")
                        .HasColumnType("int");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("T_Reservation.Models.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Dui")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("T_Reservation.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("NotaEspecial")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Producto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestauranteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("T_Reservation.Models.Mesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Capacidad")
                        .HasColumnType("int");

                    b.Property<string>("Disponibilidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("RestauranteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("T_Reservation.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantidadPersonas")
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("MesaId")
                        .HasColumnType("int");

                    b.Property<int>("RestauranteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("MenuId");

                    b.HasIndex("MesaId");

                    b.HasIndex("RestauranteId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("T_Reservation.Models.Restaurante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Imagen")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("Restaurantes");
                });

            modelBuilder.Entity("T_Reservation.Models.Cliente", b =>
                {
                    b.HasOne("T_Reservation.Models.Restaurante", "Restaurante")
                        .WithMany("Clientes")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_Reservation.Models.Menu", b =>
                {
                    b.HasOne("T_Reservation.Models.Restaurante", "Restaurante")
                        .WithMany("Menus")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_Reservation.Models.Mesa", b =>
                {
                    b.HasOne("T_Reservation.Models.Restaurante", "Restaurante")
                        .WithMany("Mesas")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_Reservation.Models.Reserva", b =>
                {
                    b.HasOne("T_Reservation.Models.Cliente", "Cliente")
                        .WithMany("Reservas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("T_Reservation.Models.Menu", null)
                        .WithMany("Reservas")
                        .HasForeignKey("MenuId");

                    b.HasOne("T_Reservation.Models.Mesa", "Mesa")
                        .WithMany("Reservas")
                        .HasForeignKey("MesaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("T_Reservation.Models.Restaurante", "Restaurante")
                        .WithMany("Reservas")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Mesa");

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_Reservation.Models.Restaurante", b =>
                {
                    b.HasOne("T_Reservation.Models.Empleado", "Empleados")
                        .WithMany("Restaurante")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("T_Reservation.Models.Cliente", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("T_Reservation.Models.Empleado", b =>
                {
                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_Reservation.Models.Menu", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("T_Reservation.Models.Mesa", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("T_Reservation.Models.Restaurante", b =>
                {
                    b.Navigation("Clientes");

                    b.Navigation("Menus");

                    b.Navigation("Mesas");

                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
