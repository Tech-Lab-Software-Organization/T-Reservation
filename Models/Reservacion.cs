namespace T_Reservation.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

    public class Reservacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        ///Relacion con cliente/

        [Required(ErrorMessage = "El campo ClienteId es obligatorio.")]
        public int ClienteId { get; set; }

        ///Relacion con Restaurante/

        [Required(ErrorMessage = "El campo RestauranteId es obligatorio.")]
        public int RestauranteId { get; set; }

        ///Relacion con Menu/

        [Required(ErrorMessage = "El campo MenuId es obligatorio.")]
        public int MenuId { get; set; }

        ///Relacion con Mesa/

        [Required(ErrorMessage = "El campo MesaId es obligatorio.")]
        public int MesaId { get; set; }

        ///Cantidad de Persona/

    [Required(ErrorMessage = "El campo CantidadPersonas es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad de personas debe ser mayor que cero.")]
        public int CantidadPersonas { get; set; }

        ///Fecha de Inicio/


    [Required(ErrorMessage = "El campo FechaInicio es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        ///Fecha de Finalizacion/


    [Required(ErrorMessage = "El campo FechaFin es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        ///Relaccion al modelo de cliente/

      /*  [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        /Relaccion al modelo de Restaurante/

    /*
        [ForeignKey("RestauranteId")]
        public virtual Restaurante Restaurante { get; set; }

        /Relaccion al modelo de Menu/
    /*
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }


        /Relaccion al modelo de Mesa/
    /*
        [ForeignKey("MesaId")]
        public virtual Mesa Mesa { get; set; }
    
    */
    }

