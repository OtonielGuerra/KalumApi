using System;
using System.ComponentModel.DataAnnotations;
using KalumNotas.Models;

namespace KalumNotas.Entities
{
    public class DetalleActividad
    {
        [Key]
        public int DetalleActividadId { get; set; }
        public int SeminarioId { get; set; }
        public string NombreActividad { get; set; }
        public int NotaActividad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaPostergacion { get; set; }
        public string Estado { get; set; }
        public Seminario Seminario { get; set; }
    }
}