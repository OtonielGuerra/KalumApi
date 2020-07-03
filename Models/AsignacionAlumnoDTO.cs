using System;
using System.ComponentModel.DataAnnotations;
using KalumNotas.Entities;

namespace KalumNotas.Models
{
    public class AsignacionAlumnoDTO
    {
        [Key]
        public int AsignacionId { get; set; }
        public int ClaseId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public AlumnoDTO Alumno { get; set; }
    }
}