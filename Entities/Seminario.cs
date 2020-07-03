using System;
using System.ComponentModel.DataAnnotations;

namespace KalumNotas.Entities
{
    public class Seminario
    {
        [Key]
        public int SeminarioId { get; set; }
        public int ModuloId { get; set; }
        public string NombreSeminario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public Modulo Modulo { get; set; }
    }
}