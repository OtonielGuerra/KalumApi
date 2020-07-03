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
    public class SeminarioController : ControllerBase // <- Implementar Controlador
    {
        #region * Constructor *
        // --- * Constructor * --- //
        private readonly IMapper Mapper;
        private readonly ILogger<Seminario> logger;
        private readonly KalumNotasBDContext DbContext;
        public SeminarioController(IMapper Mapper, ILogger<Seminario> logger, KalumNotasBDContext DbContext)
        {
            this.DbContext = DbContext;
            this.logger = logger;
            this.Mapper = Mapper;
        }
        #endregion
        #region * Get *
        // --- * HttpGet * --- //
        [HttpGet]
        public ActionResult<IEnumerable<SeminarioDTO>> Get()
        {
            logger.LogInformation("Listando Seminarios");
            var Seminario = DbContext.Seminario.Include(x => x.Modulo).ToList();
            logger.LogDebug("Retornando Lista de Seminarios");
            if (Seminario == null)
            {
                return NoContent();
            }
            else
            {
                List<SeminarioDTO> ListaSeminario = new List<SeminarioDTO>();
                foreach (var item in Seminario)
                {
                    ListaSeminario.Add(Mapper.Map<SeminarioDTO>(item));
                }
                return ListaSeminario;
            }
        }
        #endregion
        #region * Get ID *
        // --- * Get ID * --- //
        [HttpGet("{SeminarioId}", Name = "GetSeminario")]
        public ActionResult<SeminarioDTO> Get(int SeminarioId)
        {
            logger.LogInformation("Buscando un Seminario por su ID");
            var Seminario = DbContext.Seminario.FirstOrDefault(x => x.SeminarioId == SeminarioId);
            logger.LogDebug("Retornando Seminario buscado por ID");
            if (Seminario == null)
            {
                return NoContent();
            }
            else
            {
                return Mapper.Map<SeminarioDTO>(Seminario);
            }
        }
        #endregion
        #region * Post *
        // --- * Post * --- //
        [HttpPost]
        public ActionResult<SeminarioDTO> Post([FromBody] Seminario value)
        {
            logger.LogInformation("Agregando un Seminario");
            DbContext.Seminario.Add(value);
            DbContext.SaveChanges();
            logger.LogDebug("Retornando el Seminario Agregado");
            var SeminarioDto = Mapper.Map<SeminarioDTO>(value);
            return new CreatedAtRouteResult("GetSeminario", new { SeminarioDto });
        }
        #endregion
        #region * Put *
        // --- * Put * --- //
        [HttpPut("{SeminarioId}")]
        public ActionResult<SeminarioDTO> Put(int SeminarioId, [FromBody] Seminario value)
        {
            logger.LogInformation("Modificando el Seminario");
            if (SeminarioId != value.SeminarioId)
            {
                return BadRequest();
            }
            else
            {
                logger.LogDebug("Retornando Informacion Modificada");
                DbContext.Entry(value).State = EntityState.Modified;
                DbContext.SaveChanges();
                return NoContent();
            }
        }
        #endregion
        #region * Delete *
        // --- * Delete * --- //
        [HttpDelete("{SeminarioId}")]
        public ActionResult<SeminarioDTO> Delete(int SeminarioId)
        {
            logger.LogInformation("Borrando un Seminario");
            var Seminario = DbContext.Seminario.FirstOrDefault(x => x.SeminarioId == SeminarioId);
            if (Seminario == null)
            {
                return NotFound();
            }
            else
            {
                logger.LogDebug("Retornando Seminario Eliminado");
                DbContext.Seminario.Remove(Seminario);
                DbContext.SaveChanges();
                return Mapper.Map<SeminarioDTO>(Seminario);
            }
        }
        #endregion
    }
}