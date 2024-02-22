﻿using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_Reservation.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage ="el nombre no puede superar los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Dui es obligatorio")]
        [RegularExpression("[0-9]{9}", ErrorMessage ="El dui debe tener 9 digitos")]
        public int Dui { get; set; }

        [Required(ErrorMessage = "El Telefono es obligatorio")]
        [RegularExpression("[0,9]{8}", ErrorMessage ="El telefono debe tener 8 digitos")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage ="El correo electronico no es valido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(400, ErrorMessage ="La dirección no puede superar los 400 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "la fecha es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "la contraseña es obligatoria")]
        [StringLength(20, MinimumLength =8, ErrorMessage ="La contraseña debe tener 8 y 20 caracteres.")]
        [DataType(DataType.Password)]
        public string Passaword { get; set; }

    }
}