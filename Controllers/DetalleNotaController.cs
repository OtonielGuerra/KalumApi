// --- * Modelo de Controlador * --- //
// --------------------------------- //
// --- *  * --- //

// --- * Librerias * --- //
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
    public class DetalleNotaController : ControllerBase // <- Implementar Controlador
    {
        #region * Constructor *
        // --- * Constructor * --- //
        private readonly IMapper Mapper;
        private readonly ILogger<DetalleNota> logger;
        private readonly KalumNotasBDContext DbContext;
        public DetalleNotaController(IMapper Mapper, ILogger<DetalleNota> logger, KalumNotasBDContext DbContext)
        {
            this.DbContext = DbContext;
            this.logger = logger;
            this.Mapper = Mapper;
        }
        #endregion
        #region * Get *
        // --- * HttpGet * --- //
        [HttpGet]
        public ActionResult<IEnumerable<DetalleNotaDTO>> Get()
        {
            logger.LogInformation("Listando los detalles de las notas");
            var Detalle = DbContext.DetalleNota.Include(x => x.Alumno).ToList();
            logger.LogDebug("Retornando la lista de Detalle Nota");
            if (Detalle == null)
            {
                return NoContent();
            }
            else
            {
                List<DetalleNotaDTO> ListaDetalle = new List<DetalleNotaDTO>();
                foreach (var item in Detalle)
                {
                    ListaDetalle.Add(Mapper.Map<DetalleNotaDTO>(item));
                }
                return ListaDetalle;
            }
        }
        #endregion
        #region * Get ID *
        // --- * Get ID * --- //
        [HttpGet("{DetalleId}", Name = "GetDetalleNota")]
        public ActionResult<DetalleNotaDTO> Get(int DetalleId)
        {
            logger.LogInformation("Buscando un Detalle Nota");
            var Detalle = DbContext.DetalleNota.FirstOrDefault(x => x.DetalleId == DetalleId);
            logger.LogDebug("Retornando un Detalle Nota");
            if (Detalle == null)
            {
                return NoContent();
            }
            else
            {
                return Mapper.Map<DetalleNotaDTO>(Detalle);
            }
        }
        #endregion
        #region * Post *
        // --- * Post * --- //
        [HttpPost]
        public ActionResult<DetalleNotaDTO> Post([FromBody] DetalleNota value)
        {
            logger.LogInformation("Agregando un nuevo Detalle Nota");
            DbContext.DetalleNota.Add(value);
            DbContext.SaveChanges();
            logger.LogDebug("Retornando el Detalle Nota Agregado");
            var DetalleNotaDto = Mapper.Map<DetalleNotaDTO>(value);
            return new CreatedAtRouteResult("GetDetalleNota", new { DetalleNotaDto });
        }
        #endregion
        #region * Put *
        // --- * Put * --- //
        [HttpPut("{DetalleId}")]
        public ActionResult<DetalleNotaDTO> Put(int DetalleId, [FromBody] DetalleNota value)
        {
            logger.LogInformation("Modificando el Detalle Nota");
            if (DetalleId != value.DetalleId)
            {
                return BadRequest();
            }
            else
            {
                logger.LogDebug("Retornando Respuesta de Modificacion");
                DbContext.Entry(value).State = EntityState.Modified;
                DbContext.SaveChanges();
                return NoContent();
            }
        }
        #endregion
        #region * Delete *
        // --- * Delete * --- //
        [HttpDelete("{DetalleId}")]
        public ActionResult<DetalleNotaDTO> Delete(int DetalleId)
        {
            logger.LogInformation("Borrando el Detalle Nota");
            var Detalle = DbContext.DetalleNota.FirstOrDefault(x => x.DetalleId == DetalleId);
            if (Detalle == null)
            {
                return NotFound();
            }
            else
            {
                logger.LogDebug("Retornando respuesta de eliminacion Detalle Nota");
                DbContext.DetalleNota.Remove(Detalle);
                DbContext.SaveChanges();
                return Mapper.Map<DetalleNotaDTO>(Detalle);
            }
        }
        #endregion
    }
}