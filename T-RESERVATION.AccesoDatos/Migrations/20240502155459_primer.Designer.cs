﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using T_RESERVATION.AccesoDatos;

#nullable disable

namespace T_RESERVATION.AccesoDatos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240502155459_primer")]
    partial class primer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClienteRestaurante", b =>
                {
                    b.Property<int>("ClientesId")
                        .HasColumnType("int");

                    b.Property<int>("RestauranteIdRestaurante")
                        .HasColumnType("int");

                    b.HasKey("ClientesId", "RestauranteIdRestaurante");

                    b.HasIndex("RestauranteIdRestaurante");

                    b.ToTable("RestauranteCliente", (string)null);
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Cliente", b =>
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

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Empleado", b =>
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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empleados");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Correo = "root@gmail.com",
                            Direccion = "NINGUNA,NINGUNA",
                            Dui = 234555321,
                            FechaNacimiento = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "root",
                            Password = "25d55ad283aa400af464c76d713c07ad",
                            Rol = "Administrador",
                            Telefono = 22229090
                        });
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Menu", b =>
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
                        .IsRequired()
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

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Mesa", b =>
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

                    b.Property<int>("IdRestaurante")
                        .HasColumnType("int");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdRestaurante");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Reserva", b =>
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

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Restaurante", b =>
                {
                    b.Property<int>("IdRestaurante")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRestaurante"), 1L, 1);

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
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdRestaurante");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("Restaurantes");
                });

            modelBuilder.Entity("ClienteRestaurante", b =>
                {
                    b.HasOne("T_RESERVATION.EntidadesNegocio.Cliente", null)
                        .WithMany()
                        .HasForeignKey("ClientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("T_RESERVATION.EntidadesNegocio.Restaurante", null)
                        .WithMany()
                        .HasForeignKey("RestauranteIdRestaurante")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Menu", b =>
                {
                    b.HasOne("T_RESERVATION.EntidadesNegocio.Restaurante", "Restaurante")
                        .WithMany("Menus")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Mesa", b =>
                {
                    b.HasOne("T_RESERVATION.EntidadesNegocio.Restaurante", "Restaurante")
                        .WithMany("Mesas")
                        .HasForeignKey("IdRestaurante")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Reserva", b =>
                {
                    b.HasOne("T_RESERVATION.EntidadesNegocio.Cliente", "Cliente")
                        .WithMany("Reservas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("T_RESERVATION.EntidadesNegocio.Menu", null)
                        .WithMany("Reservas")
                        .HasForeignKey("MenuId");

                    b.HasOne("T_RESERVATION.EntidadesNegocio.Mesa", "Mesa")
                        .WithMany("Reservas")
                        .HasForeignKey("MesaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("T_RESERVATION.EntidadesNegocio.Restaurante", "Restaurante")
                        .WithMany("Reservas")
                        .HasForeignKey("RestauranteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Mesa");

                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Restaurante", b =>
                {
                    b.HasOne("T_RESERVATION.EntidadesNegocio.Empleado", "Empleados")
                        .WithMany("Restaurante")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Cliente", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Empleado", b =>
                {
                    b.Navigation("Restaurante");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Menu", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Mesa", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("T_RESERVATION.EntidadesNegocio.Restaurante", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Mesas");

                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
