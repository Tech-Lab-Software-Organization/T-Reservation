using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T_Reservation.Models
{
    public class Login
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Idlogin { get; set; }
        [Required]
        public string Correo { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
