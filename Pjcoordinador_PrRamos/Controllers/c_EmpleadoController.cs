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
    public class c_EmpleadoController : ApiController
    {
        private DBcoordinador_RamosEntities3 db = new DBcoordinador_RamosEntities3();

        // GET: api/c_Empleado
        public IQueryable<c_Empleado> Getc_Empleado()
        {
            return db.c_Empleado;
        }

        // GET: api/c_Empleado/5
        [ResponseType(typeof(c_Empleado))]
        public IHttpActionResult Getc_Empleado(int id)
        {
            c_Empleado c_Empleado = db.c_Empleado.Find(id);
            if (c_Empleado == null)
            {
                return NotFound();
            }

            return Ok(c_Empleado);
        }

        // PUT: api/c_Empleado/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putc_Empleado(int id, c_Empleado c_Empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != c_Empleado.idEmpleado)
            {
                return BadRequest();
            }

            db.Entry(c_Empleado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!c_EmpleadoExists(id))
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

        // POST: api/c_Empleado
        [ResponseType(typeof(c_Empleado))]
        public IHttpActionResult Postc_Empleado(c_Empleado c_Empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.c_Empleado.Add(c_Empleado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = c_Empleado.idEmpleado }, c_Empleado);
        }

        // DELETE: api/c_Empleado/5
        [ResponseType(typeof(c_Empleado))]
        public IHttpActionResult Deletec_Empleado(int id)
        {
            c_Empleado c_Empleado = db.c_Empleado.Find(id);
            if (c_Empleado == null)
            {
                return NotFound();
            }

            db.c_Empleado.Remove(c_Empleado);
            db.SaveChanges();

            return Ok(c_Empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool c_EmpleadoExists(int id)
        {
            return db.c_Empleado.Count(e => e.idEmpleado == id) > 0;
        }
    }
}