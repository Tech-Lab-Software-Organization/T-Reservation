﻿namespace T_Reservation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Menu
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo{0} es requerido")]
    public string Producto { get; set; }

    // [Required(ErrorMessage = "El campo{0} es requerido")]
    public byte[] Imagen { get; set; }
    [Required(ErrorMessage = "El campo{0} es requerido")]
    [StringLength(500)]
    public string Descripcion { get; set; }

    [StringLength(200)]
    [Required(ErrorMessage = "El campo{0} es requerido")]
    public string NotaEspecial { get; set; }
    [Required(ErrorMessage = "El campo{0} es requerido")]
    public decimal Precio { get; set; }

    public int RestautanteId { get; set; }
    //[ForeignKey("RestauranteId)]
    //public Restaurantes restaurante { get; set; }
}
