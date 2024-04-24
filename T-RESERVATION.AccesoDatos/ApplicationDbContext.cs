using Microsoft.EntityFrameworkCore;
using T_RESERVATION.EntidadesNegocio;


namespace T_RESERVATION.AccesoDatos
{
    public class ApplicationDbContext : DbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().HasData(
                new Empleado { Correo = "root@gmail.com", 
                    Id = 1, Nombre = "root", Dui = 234555321, 
                    FechaNacimiento = new DateTime(1990, 1, 1),
                    Direccion="NINGUNA,NINGUNA",
                    Telefono=22229090, 
                    Rol = "Administrador", 
                    Password = "25d55ad283aa400af464c76d713c07ad"
                }
                );
            //Configuración de la relación entre Empleado y Restaurante ( 1: M)/
            modelBuilder.Entity<Restaurante>()
                .HasOne(r => r.Empleados)
                .WithMany(o => o.Restaurante)
                .HasForeignKey(r => r.EmpleadoId);

            //Configuración de la relación entre Restaurante y Menu (1:M)/
            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Restaurante)
                .WithMany(r => r.Menus)
                .HasForeignKey(m => m.RestauranteId);

            //Configuración de la relación entre Restaurante y Mesa (1:M)/
            modelBuilder.Entity<Restaurante>()
        .HasMany(r => r.Mesas)
        .WithOne(m => m.Restaurante)
        .HasForeignKey(m => m.IdRestaurante);
            //Configuración de la relación entre Mesa y Reserva (1:M)/
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Mesa)
                .WithMany(t => t.Reservas)
                .HasForeignKey(r => r.MesaId)
                .OnDelete(DeleteBehavior.Restrict); // Se especifica que no se permite la eliminación en cascada
                

            //Configuración de la relación entre Restaurante y Reserva (1:M)/
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Restaurante)
                .WithMany(r => r.Reservas)
                .HasForeignKey(r => r.RestauranteId)
                .OnDelete(DeleteBehavior.Restrict);

            //Configuración de la relación entre Cliente y Reserva (1:M)/
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            //relación M:M entre restaurante y cliente, utilizando una tabla de unión implícita RestauranteCliente
            modelBuilder.Entity<Restaurante>()
            .HasMany(r => r.Clientes)
            .WithMany(c => c.Restaurante)
            .UsingEntity(j => j.ToTable("RestauranteCliente"));

            base.OnModelCreating(modelBuilder);
        }

   
    }
}
