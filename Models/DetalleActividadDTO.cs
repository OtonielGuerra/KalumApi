using System;
using System.ComponentModel.DataAnnotations;
using KalumNotas.Entities;

namespace KalumNotas.Models
{
    public class DetalleActividadDTO
    {
        [Key]
        public int DetalleActividadId { get; set; }
        public string NombreActividad { get; set; }
        public int NotaActividad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPostergacion { get; set; }
        public string Estado { get; set; }
        public SeminarioDTO Seminario { get; set; }
    }
}