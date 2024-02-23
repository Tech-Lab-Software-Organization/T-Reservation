using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_Reservation.Models
{
    public class Mesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
      
        [Required(ErrorMessage = "El campo {0} es requerido")]

        public int Numero { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        public int Capacidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]

        [StringLength(100)]
        public string Area { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        
        public string Disponibilidad { get; set; }

        [ForeignKey("Restaurante")]
        public int RestauranteId { get; set; }
        public Restaurante Restaurante { get; set; }

        public virtual ICollection<Reserva> Reservaciones { get; set; }

    }
}
