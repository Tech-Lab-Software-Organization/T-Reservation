﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace T_Reservation.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El DUI es obligatorio")]
        [RegularExpression("[0-9]{9}", ErrorMessage = "El DUI debe tener 9 dígitos")]
        public int Dui { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression("[0-9]{8}", ErrorMessage = "El teléfono debe tener 8 dígitos")]
        public int Telefono { get; set; }

       
        public string Rol { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener 6 y 100 caracteres.")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]//esta propiedad no sera mapeada en la base de datos.
        [Compare("Password", ErrorMessage = "La contraseña no coincide")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener 6 y 100 caracteres.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public ICollection<Restaurante> Restaurante { get; set; }

        public Empleado()
        {

            Rol = "Empleado";
        }
    }
}
