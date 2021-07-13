using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Pjcoordinador_PrRamos.Models;

namespace Pjcoordinador_PrRamos.Controllers
{
    public class t_RequisicionesController : ApiController
    {
        private DBcoordinador_RamosEntities2 db = new DBcoordinador_RamosEntities2();

        // GET: api/t_Requisiciones
        public IQueryable<t_Requisiciones> Gett_Requisiciones()
        {
            return db.t_Requisiciones;
        }

        // GET: api/t_Requisiciones/5
        [ResponseType(typeof(t_Requisiciones))]
        public IHttpActionResult Gett_Requisiciones(int id)
        {
            t_Requisiciones t_Requisiciones = db.t_Requisiciones.Find(id);
            if (t_Requisiciones == null)
            {
                return NotFound();
            }

            return Ok(t_Requisiciones);
        }

        // PUT: api/t_Requisiciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putt_Requisiciones(int id, t_Requisiciones t_Requisiciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_Requisiciones.idRequisicion)
            {
                return BadRequest();
            }

            db.Entry(t_Requisiciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_RequisicionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/t_Requisiciones
        [ResponseType(typeof(t_Requisiciones))]
        public IHttpActionResult Postt_Requisiciones(t_Requisiciones t_Requisiciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.t_Requisiciones.Add(t_Requisiciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = t_Requisiciones.idRequisicion }, t_Requisiciones);
        }

        // DELETE: api/t_Requisiciones/5
        [ResponseType(typeof(t_Requisiciones))]
        public IHttpActionResult Deletet_Requisiciones(int id)
        {
            t_Requisiciones t_Requisiciones = db.t_Requisiciones.Find(id);
            if (t_Requisiciones == null)
            {
                return NotFound();
            }

            db.t_Requisiciones.Remove(t_Requisiciones);
            db.SaveChanges();

            return Ok(t_Requisiciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool t_RequisicionesExists(int id)
        {
            return db.t_Requisiciones.Count(e => e.idRequisicion == id) > 0;
        }
    }
}