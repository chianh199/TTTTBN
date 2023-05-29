using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class CHITIETPHANQUYENsController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        // GET: api/CHITIETPHANQUYENs
        public IQueryable<CHITIETPHANQUYEN> GetCHITIETPHANQUYENs()
        {
            return db.CHITIETPHANQUYENs;
        }

        // GET: api/CHITIETPHANQUYENs/5
        [ResponseType(typeof(CHITIETPHANQUYEN))]
        public async Task<IHttpActionResult> GetCHITIETPHANQUYEN(int id)
        {
            CHITIETPHANQUYEN cHITIETPHANQUYEN = await db.CHITIETPHANQUYENs.FindAsync(id);
            if (cHITIETPHANQUYEN == null)
            {
                return NotFound();
            }

            return Ok(cHITIETPHANQUYEN);
        }

        // PUT: api/CHITIETPHANQUYENs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCHITIETPHANQUYEN(int id, CHITIETPHANQUYEN cHITIETPHANQUYEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cHITIETPHANQUYEN.IDCHITIETPHANQUYEN)
            {
                return BadRequest();
            }

            db.Entry(cHITIETPHANQUYEN).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CHITIETPHANQUYENExists(id))
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

        // POST: api/CHITIETPHANQUYENs
        [ResponseType(typeof(CHITIETPHANQUYEN))]
        public async Task<IHttpActionResult> PostCHITIETPHANQUYEN(CHITIETPHANQUYEN cHITIETPHANQUYEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CHITIETPHANQUYENs.Add(cHITIETPHANQUYEN);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cHITIETPHANQUYEN.IDCHITIETPHANQUYEN }, cHITIETPHANQUYEN);
        }

        // DELETE: api/CHITIETPHANQUYENs/5
        [ResponseType(typeof(CHITIETPHANQUYEN))]
        public async Task<IHttpActionResult> DeleteCHITIETPHANQUYEN(int id)
        {
            CHITIETPHANQUYEN cHITIETPHANQUYEN = await db.CHITIETPHANQUYENs.FindAsync(id);
            if (cHITIETPHANQUYEN == null)
            {
                return NotFound();
            }

            db.CHITIETPHANQUYENs.Remove(cHITIETPHANQUYEN);
            await db.SaveChangesAsync();

            return Ok(cHITIETPHANQUYEN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CHITIETPHANQUYENExists(int id)
        {
            return db.CHITIETPHANQUYENs.Count(e => e.IDCHITIETPHANQUYEN == id) > 0;
        }
    }
}