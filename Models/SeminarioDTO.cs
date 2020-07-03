using System;
using System.ComponentModel.DataAnnotations;
using KalumNotas.Entities;

namespace KalumNotas.Models
{
    public class SeminarioDTO
    {
        [Key]
        public int SeminarioId { get; set; }
        public string NombreSeminario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public Modulo Modulo { get; set; }
    }
}