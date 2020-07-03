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
    public class DetalleActividadController : ControllerBase // <- Implementar Controlador
    {
        #region * Constructor *
        // --- * Constructor * --- //
        private readonly IMapper Mapper;
        private readonly ILogger<DetalleActividad> logger;
        private readonly KalumNotasBDContext DbContext;
        public DetalleActividadController(IMapper Mapper, ILogger<DetalleActividad> logger, KalumNotasBDContext DbContext)
        {
            this.DbContext = DbContext;
            this.logger = logger;
            this.Mapper = Mapper;
        }
        #endregion
        #region * Get *
        // --- * HttpGet * --- //
        [HttpGet]
        public ActionResult<IEnumerable<DetalleActividadDTO>> Get()
        {
            logger.LogInformation("Listando Detalles de la Actividad");
            var Detalle = DbContext.DetalleActividad.Include(x => x.Seminario).ToList();
            logger.LogDebug("Resultado de la lista de DetalleActividad");
            if (Detalle == null)
            {
                return NoContent();
            }
            else
            {
                List<DetalleActividadDTO> ListDetalle = new List<DetalleActividadDTO>();
                foreach (var registro in Detalle)
                {
                    ListDetalle.Add(Mapper.Map<DetalleActividadDTO>(registro));
                }
                return ListDetalle;
            }
        }
        #endregion
        #region * Get ID *
        // --- * Get ID * --- //
        [HttpGet("{DetalleId}", Name = "GetDetalleActividad")]
        public ActionResult<DetalleActividadDTO> Get(int DetalleId)
        {
            logger.LogInformation("Buscando DetalleActividad");
            var Detalle = DbContext.DetalleActividad.FirstOrDefault(x => x.DetalleActividadId == DetalleId);
            logger.LogDebug("Retornando Detalle Actividad");
            if (Detalle == null)
            {
                return NoContent();
            }
            else
            {
                return Mapper.Map<DetalleActividadDTO>(Detalle);
            }
        }
        #endregion
        #region * Post *
        // --- * Post * --- //
        [HttpPost]
        public ActionResult<DetalleActividadDTO> Post([FromBody] DetalleActividad value)
        {
            logger.LogInformation("Agregando Detalle Actividad");
            DbContext.DetalleActividad.Add(value);
            DbContext.SaveChanges();
            logger.LogDebug("Retornando la información de el Detalle de Actividad Agregado");
            var DetalleDto = Mapper.Map<DetalleActividadDTO>(value);
            return new CreatedAtRouteResult("GetDetalleActividad", new { DetalleDto });
        }
        #endregion
        #region * Put *
        // --- * Put * --- //
        [HttpPut("{DetalleId}")]
        public ActionResult<DetalleActividad> Put(int DetalleId, [FromBody] DetalleActividad value)
        {
            logger.LogInformation("Modificando el Detalle de Actividad");
            if (DetalleId != value.DetalleActividadId)
            {
                return BadRequest();
            }
            else
            {
                logger.LogDebug("Retornando Detalle Actividad Modificado");
                DbContext.Entry(value).State = EntityState.Modified;
                DbContext.SaveChanges();
                return NoContent();
            }
        }
        #endregion
        #region * Delete *
        // --- * Delete * --- //
        [HttpDelete("{DetalleId}")]
        public ActionResult<DetalleActividadDTO> Delete(int DetalleId)
        {
            logger.LogInformation("Eliminando Detalle Actividad");
            var Detalle = DbContext.DetalleActividad.FirstOrDefault(x => x.DetalleActividadId == DetalleId);
            if (Detalle == null)
            {
                return NotFound();
            }
            else
            {
                logger.LogDebug("Retornando Detalle Actividad Eliminado");
                DbContext.DetalleActividad.Remove(Detalle);
                DbContext.SaveChanges();
                return Mapper.Map<DetalleActividadDTO>(Detalle);
            }
        }
        #endregion
    }
}