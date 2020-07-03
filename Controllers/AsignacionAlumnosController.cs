using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KalumNotas.Entities;
using KalumNotas.KalumBDContext;
using KalumNotas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KalumNotas.Controllers
{
    [Route("/KalumNotas/v1.0/[controller]")]
    public class AsignacionAlumnosController : ControllerBase
    {
        #region * Constructor *
        private readonly KalumNotasBDContext dbContext;
        private readonly IMapper Mapper;
        private readonly ILogger Logger;
        public AsignacionAlumnosController(IMapper Mapper, KalumNotasBDContext DbContext, ILogger Logger)
        {
            this.Logger = Logger;
            this.Mapper = Mapper;
            this.dbContext = DbContext;
        }
        #endregion
        #region * HttpGet *
        [HttpGet]
        public ActionResult<IEnumerable<AsignacionAlumnoDTO>> Get()
        {
            var Asignacion = dbContext.AsignacionAlumnos.Include(x => x.Alumno).ToList();
            if (Asignacion == null)
            {
                return NoContent();
            }
            else
            {
                Logger.LogInformation("Listando las Asignaciones");
                List<AsignacionAlumnoDTO> ListaAsignacionesDTO = new List<AsignacionAlumnoDTO>();
                foreach (var registro in Asignacion)
                {
                    ListaAsignacionesDTO.Add(Mapper.Map<AsignacionAlumnoDTO>(registro));
                }
                Logger.LogDebug("Retornando la información Listada");
                return ListaAsignacionesDTO;
            }
        }
        #endregion
        #region * HttpGet (Busqueda) *
        [HttpGet("{Asignacion}", Name = "GetAsignacionAlumno")]
        public ActionResult<AsignacionAlumnoDTO> Get(int Asignacion)
        {
            Logger.LogInformation("Buscando Asignacion Alumno por medio de su ID");
            var _Asignacion = dbContext.AsignacionAlumnos.FirstOrDefault(x => x.AsignacionId == Asignacion);
            if (_Asignacion == null)
            {
                return NotFound();
            }
            else
            {
                Logger.LogDebug("Retornando las Asignacion Buscada");
                //List<AsignacionAlumnoDTO> lista = Mapper.Map<List<AsignacionAlumnoDTO>>(_Asignacion);
                return Mapper.Map<AsignacionAlumnoDTO>(_Asignacion);
            }
        }
        #endregion
        #region * HttpPost *
        [HttpPost]
        public ActionResult<AsignacionAlumno> Post([FromBody] AsignacionAlumno value)
        {
            Logger.LogInformation("Creando una Asignacion Nueva");
            dbContext.AsignacionAlumnos.Add(value);
            dbContext.SaveChanges();
            Logger.LogDebug("Retornar la información que fue creada");
            return new CreatedAtRouteResult("GetAsignacionAlumno", new { Asignacion = value.AsignacionId }, value); ;
        }
        #endregion
        #region * HttpPut *
        [HttpPut("{asignacionId}")]
        public ActionResult<AsignacionAlumno> Put(int asignacionId, [FromBody] AsignacionAlumno value)
        {
            Logger.LogInformation("Modificando al alumno");
            if (asignacionId != value.AsignacionId)
            {
                return BadRequest();
            }
            else
            {
                Logger.LogDebug("Guardando la información del alumno");
                dbContext.Entry(value).State = EntityState.Modified;
                dbContext.SaveChanges();
                return NoContent();
            }
        }
        #endregion
        #region * HttpDelete *
        [HttpDelete("{asignacionId}")]
        public ActionResult<AsignacionAlumno> Delete(int asignacionId)
        {
            Logger.LogInformation("Borrando datos de alumno");
            var Asignacion = dbContext.AsignacionAlumnos.FirstOrDefault(x => x.AsignacionId == asignacionId);
            if (Asignacion == null)
            {
                return NotFound();
            }
            else
            {
                Logger.LogDebug("Guardando cambios de alumno eliminado");
                dbContext.AsignacionAlumnos.Remove(Asignacion);
                dbContext.SaveChanges();
                return Asignacion;
            }
        }
        #endregion
    }
}