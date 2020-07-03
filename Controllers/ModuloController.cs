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
    public class ModuloController : ControllerBase // <- Implementar Controlador
    {
        #region * Constructor *
        // --- * Constructor * --- //
        private readonly IMapper Mapper;
        private readonly ILogger<Modulo> logger;
        private readonly KalumNotasBDContext DbContext;
        public ModuloController(IMapper Mapper, ILogger<Modulo> logger, KalumNotasBDContext DbContext)
        {
            this.DbContext = DbContext;
            this.logger = logger;
            this.Mapper = Mapper;
        }
        #endregion
        #region * Get *
        // --- * HttpGet * --- //
        [HttpGet]
        public ActionResult<IEnumerable<Modulo>> Get()
        {
            var Modulo = DbContext.Modulo.ToList();
            if (Modulo == null)
            {
                return NoContent();
            }
            else{
                return Modulo;
            }
        }
        #endregion
        #region * Get ID *
        // --- * Get ID * --- //
        [HttpGet("{ModuloId}", Name = "GetModulo")]
        public ActionResult<Modulo> Get(int ModuloId)
        {
            var Modulo = DbContext.Modulo.FirstOrDefault(x => x.ModuloId == ModuloId);
            if (Modulo == null)
            {
                return NoContent();
            }
            else
            {
                return Modulo;
            }
        }
        #endregion
        #region * Post *
        // --- * Post * --- //
        [HttpPost]
        public ActionResult<Modulo> Post([FromBody] Modulo value)
        {
            DbContext.Modulo.Add(value);
            DbContext.SaveChanges();
            return new CreatedAtRouteResult("GetModulo", new { value });
        }
        #endregion
        #region * Put *
        // --- * Put * --- //
        [HttpPut("{ModuloId}")]
        public ActionResult<Modulo> Put(int ModuloId, [FromBody] Modulo value)
        {
            if (ModuloId != value.ModuloId)
            {
                return BadRequest();
            }
            else
            {
                DbContext.Entry(value).State = EntityState.Modified;
                DbContext.SaveChanges();
                return NoContent();
            }
        }
        #endregion
        #region * Delete *
        // --- * Delete * --- //
        [HttpDelete("{ModuloId}")]
        public ActionResult<Modulo> Delete(int ModuloId)
        {
            var Modulo = DbContext.Modulo.FirstOrDefault(x => x.ModuloId == ModuloId);
            if (Modulo == null)
            {
                return NotFound();
            }
            else
            {
                DbContext.Modulo.Remove(Modulo);
                DbContext.SaveChanges();
                return Modulo;
            }
        }
        #endregion
    }
}