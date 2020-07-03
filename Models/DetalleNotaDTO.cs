using System.ComponentModel.DataAnnotations;

namespace KalumNotas.Models
{
    public class DetalleNotaDTO
    {
        [Key]
        public int DetalleId { get; set; }
        public int DetalleActividadId { get; set; }
        public int ValorNota { get; set; }
        public AlumnoDTO Alumno { get; set; }
    }
}