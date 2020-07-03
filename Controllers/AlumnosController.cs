using Microsoft.AspNetCore.Mvc;
using KalumNotas.KalumBDContext;
using System.Collections.Generic;
using KalumNotas.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using KalumNotas.Models;
using AutoMapper;

namespace KalumNotas.Controllers
{
    [Route("/KalumNotas/v1.0/[controller]")]
    public class AlumnosController : ControllerBase
    {
        #region * Constructor *
        private readonly KalumNotasBDContext DbContext;
        private readonly ILogger<AlumnosController> logger;
        private readonly IMapper mapper;
        public AlumnosController(IMapper mapper, ILogger<AlumnosController> logger, KalumNotasBDContext DbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.DbContext = DbContext;
        }
        #endregion
        #region * HttpGet *
        [HttpGet]
        public ActionResult<IEnumerable<AlumnoDTO>> Get()
        {
            logger.LogInformation("Iniciando Proceso de consulta de alumnos");
            var alumnos = DbContext.Alumnos.ToList();
            if (alumnos == null)
            {
                return NoContent();
            }
            else
            {
                logger.LogDebug("Haciendo consulta de datos sin filtro");
                List<AlumnoDTO> AlumnosDto = new List<AlumnoDTO>();;
                foreach (var alumno in alumnos)
                {
                    AlumnosDto.Add(mapper.Map<AlumnoDTO>(alumno));
                }
                return AlumnosDto;
            }
        }
        #endregion
        #region * HttpGet (Buscar) *
        [HttpGet("{alumnoId}", Name = "GetAlumno")]
        public ActionResult<AlumnoDTO> Get(int alumnoId)
        {
            logger.LogInformation("Haciendo consulta de datos con el filtro del ID Alumno");
            var Alumno = DbContext.Alumnos.FirstOrDefault(x => x.AlumnoId == alumnoId);
            if (Alumno == null)
            {
                logger.LogDebug("No se encontró un Alumno con el ID asignado");
                return NotFound();
            }
            else
            {
                logger.LogDebug("Finalizando proceso de busqueda");
                return mapper.Map<AlumnoDTO>(Alumno);
            }
        }
        #endregion
        #region * HttpPost *
        [HttpPost]
        public ActionResult<AlumnoDTO> Post([FromBody] Alumno value)
        {
            logger.LogInformation("Agregando Alumno a la DB");
            DbContext.Alumnos.Add(value);
            DbContext.SaveChanges();
            logger.LogDebug("Convirtiendo el Alumno a AlumnoDTO, para una mejor vista");
            var alumnoDTO = mapper.Map<AlumnoDTO>(value);
            return new CreatedAtRouteResult("GetAlumno", new { alumnoId = value.AlumnoId }, alumnoDTO);
        }
        #endregion
        #region * HttpPut *
        [HttpPut("{alumnoId}")]
        public ActionResult<Alumno> Put(int alumnoId, [FromBody] Alumno value)
        {
            logger.LogInformation("Modificando los datos de Alumno");
            if (alumnoId != value.AlumnoId)
            {
                return BadRequest();
            }
            else
            {
                logger.LogDebug("Guardando los cambios de la data Modificada");
                DbContext.Entry(value).State = EntityState.Modified;
                DbContext.SaveChanges();
                return NoContent();
            }
        }
        #endregion
        #region * HttpDelete *
        [HttpDelete("{alumnoId}")]
        public ActionResult<AlumnoDTO> Delete(int alumnoId)
        {
            logger.LogInformation("Borrando la información del Alumno");
            var Alumno = DbContext.Alumnos.FirstOrDefault(x => x.AlumnoId == alumnoId);
            if (Alumno == null)
            {
                return NotFound();
            }
            else
            {
                logger.LogDebug("Guardando los cambios del Alumno eliminado");
                DbContext.Alumnos.Remove(Alumno);
                DbContext.SaveChanges();
                var alumnoDto = mapper.Map<AlumnoDTO>(Alumno);
                return alumnoDto;
            }
        }
        #endregion
    }
}