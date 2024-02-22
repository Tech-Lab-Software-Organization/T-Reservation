namespace T_Reservation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    public class Restaurante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Descripcion es obligatorio")]
        [StringLength(500)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El Direccion es obligatorio")]
        [StringLength(250)]
        public string Direccion { get; set; }
    }

