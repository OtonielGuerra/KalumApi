using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KalumNotas.Entities
{
    public class Alumno
    {
        [Key]
        public int AlumnoId { get; set; }
        public int Carne { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public List<AsignacionAlumno> AsignacionAlumnos { get; set; }
    }
}