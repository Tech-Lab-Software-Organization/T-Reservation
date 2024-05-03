using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using T_RESERVATION.EntidadesNegocio;

namespace T_Reservation.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "el nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Dui es obligatorio")]
        [RegularExpression("[0-9]{9}", ErrorMessage = "El DUI debe contener 9 dígitos")]
        public int Dui { get; set; }

        public bool IsValidDui(int dui)
        {
            string duiString = dui.ToString();
            return duiString.Length == 9 && !duiString.StartsWith("0");
        }




        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "El teléfono debe tener 8 dígitos")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electronico no es valido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(400, ErrorMessage = "La dirección no puede superar los 400 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "la fecha es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayName("Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }


        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener 6 y 100 caracteres.")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]//esta propiedad no sera mapeada en la base de datos.
        [Compare("Password", ErrorMessage = "La contraseña no coincide")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener 6 y 100 caracteres.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar contraseña")]
        public string ConfirmPassword { get; set; }

        public string Rol { get; set; }

        public ICollection<Restaurante> Restaurante { get; set; }

        public ICollection<Reserva> Reservas { get; set; }

        public Cliente()
        {

            Rol = "Cliente";
        }

    }
    
}
