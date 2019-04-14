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
using AlumnosWebApp.Models;

namespace labo.Controllers
{
    public class TareaAlumnoController : ApiController
    {
        private AlumnosWebAppContext db = new AlumnosWebAppContext();

        // GET: api/TareaAlumno
        public IQueryable<TareaAlumno> GetTareaAlumnoes()
        {
            return db.TareaAlumnoes;
        }

        // GET: api/TareaAlumno/5
        [ResponseType(typeof(TareaAlumno))]
        public IHttpActionResult GetTareaAlumno(int id)
        {
            TareaAlumno tareaAlumno = db.TareaAlumnoes.Find(id);
            if (tareaAlumno == null)
            {
                return NotFound();
            }

            return Ok(tareaAlumno);
        }

        // PUT: api/TareaAlumno/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTareaAlumno(int id, TareaAlumno tareaAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tareaAlumno.IdTarea)
            {
                return BadRequest();
            }

            db.Entry(tareaAlumno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaAlumnoExists(id))
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

        // POST: api/TareaAlumno
        [ResponseType(typeof(TareaAlumno))]
        public IHttpActionResult PostTareaAlumno(TareaAlumno tareaAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TareaAlumnoes.Add(tareaAlumno);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TareaAlumnoExists(tareaAlumno.IdTarea))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tareaAlumno.IdTarea }, tareaAlumno);
        }

        // DELETE: api/TareaAlumno/5
        [ResponseType(typeof(TareaAlumno))]
        public IHttpActionResult DeleteTareaAlumno(int id)
        {
            TareaAlumno tareaAlumno = db.TareaAlumnoes.Find(id);
            if (tareaAlumno == null)
            {
                return NotFound();
            }

            db.TareaAlumnoes.Remove(tareaAlumno);
            db.SaveChanges();

            return Ok(tareaAlumno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TareaAlumnoExists(int id)
        {
            return db.TareaAlumnoes.Count(e => e.IdTarea == id) > 0;
        }
    }
}