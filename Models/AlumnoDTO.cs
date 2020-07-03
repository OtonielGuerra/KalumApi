using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KalumNotas.Entities;

namespace KalumNotas.Models
{
    public class AlumnoDTO
    {
        [Key]
        public int AlumnoId { get; set; }
        public string FullName { get; set; }
        public int Carne { get; set; }
        public string Email { get; set; }
    }
}