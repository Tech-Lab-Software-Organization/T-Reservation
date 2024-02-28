using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace T_Reservation.Models
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
            modelBuilder.Entity<Mesa>()
                .HasOne(t => t.Restaurante)
                .WithMany(r => r.Mesas)
                .HasForeignKey(t => t.RestauranteId);

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
        }

        public string ValidarLogin(string correo, string password)
        {
            // PA CLIENTE
            var cliente = Clientes.FirstOrDefault(c => c.Correo == correo && c.Passaword == password);
            if (cliente != null)
                return cliente.Nombre;

            // PA EMPLEADO
            var empleado = Empleados.FirstOrDefault(e => e.Correo == correo && e.Password == password);
            return empleado != null ? empleado.Nombre : null;
        }

    }
}
