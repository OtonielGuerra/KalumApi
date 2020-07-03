using System.ComponentModel.DataAnnotations;
using KalumNotas.Models;

namespace KalumNotas.Entities
{
    public class DetalleNota
    {
        [Key]
        public int DetalleId { get; set; }
        public int DetalleActividadId { get; set; }
        public int AlumnoId { get; set; }
        public int ValorNota { get; set; }
        public Alumno Alumno { get; set; }
    }
}