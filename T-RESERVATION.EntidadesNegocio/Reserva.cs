using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_Reservation.Models
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //**Relacion con cliente**

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [ForeignKey("ClienteId")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public  Cliente Cliente { get; set; }

        //**Relacion con Restaurante**

        [Required(ErrorMessage = "El campo Restaurante es obligatorio.")]
        [Display(Name = "Restaurante")]
        [ForeignKey("RestauranteId")]
        public int RestauranteId { get; set; }
        public  Restaurante Restaurante { get; set; }


        [Required(ErrorMessage = "El campo Mesa es obligatorio.")]
        [Display(Name = "Mesa")]
        [ForeignKey("MesaId")]
        public int MesaId { get; set; }
        public  Mesa Mesa { get; set; }

        //**Cantidad de Persona**
        [Display(Name = "Cantidad De Personas")]
        [Required(ErrorMessage = "El campo de Cantidad Personas es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de personas debe ser mayor que cero.")]
       public int CantidadPersonas { get; set; }

        //**Fecha de Inicio**
        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "El campo de Fecha Inicio es obligatorio.")]
        [DataType(DataType.Date)]
       public DateTime FechaInicio { get; set; }

        //**Fecha de Finalizacion**
        [Display(Name = "Fecha Final")]
        [Required(ErrorMessage = "El campo de Fecha Fin es obligatorio.")]
        [DataType(DataType.Date)]
       public DateTime FechaFin { get; set; }
        

        /*Relaccion al modelo de cliente*/

        /*  [ForeignKey("ClienteId")]
          public virtual Cliente Cliente { get; set; }
          /*Relaccion al modelo de Restaurante*/

        /*
            [ForeignKey("RestauranteId")]
            public virtual Restaurante Restaurante { get; set; }
            /*Relaccion al modelo de Menu*/
        /*
            [ForeignKey("MenuId")]
            public virtual Menu Menu { get; set; }
            /*Relaccion al modelo de Mesa*/
        /*
            [ForeignKey("MesaId")]
            public virtual Mesa Mesa { get; set; }

        */
    }
}