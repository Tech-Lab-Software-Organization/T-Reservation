namespace T_Reservation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Restaurante
{
    public Restaurante()
    {
        Mesas = new List<Mesa>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdRestaurante { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; }

    public byte[] Imagen { get; set; }

    [Required(ErrorMessage = "El Descripcion es obligatorio ")]
    [StringLength(500)]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El Direccion es obligatorio")]
    [StringLength(250)]
    public string Direccion { get; set; }

    public ICollection<Cliente> Clientes { get; set; }

    [ForeignKey("EmpleadoId")]
    public int EmpleadoId { get; set; }
    public Empleado Empleados { get; set; }

    public ICollection<Menu> Menus { get; set; }
    public IList<Mesa> Mesas { get; set; }

    public ICollection<Reserva> Reservas { get; set; }

}