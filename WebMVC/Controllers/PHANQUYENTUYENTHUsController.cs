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
    public class PHANQUYENTUYENTHUsController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        // GET: api/PHANQUYENTUYENTHUs
        public IQueryable<PHANQUYENTUYENTHU> GetPHANQUYENTUYENTHUs()
        {
            return db.PHANQUYENTUYENTHUs;
        }

        // GET: api/PHANQUYENTUYENTHUs/5
        [ResponseType(typeof(PHANQUYENTUYENTHU))]
        public async Task<IHttpActionResult> GetPHANQUYENTUYENTHU(int id)
        {
            PHANQUYENTUYENTHU pHANQUYENTUYENTHU = await db.PHANQUYENTUYENTHUs.FindAsync(id);
            if (pHANQUYENTUYENTHU == null)
            {
                return NotFound();
            }

            return Ok(pHANQUYENTUYENTHU);
        }

        // PUT: api/PHANQUYENTUYENTHUs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPHANQUYENTUYENTHU(int id, PHANQUYENTUYENTHU pHANQUYENTUYENTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pHANQUYENTUYENTHU.IDPHANQUYENTUYENTHU)
            {
                return BadRequest();
            }

            db.Entry(pHANQUYENTUYENTHU).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PHANQUYENTUYENTHUExists(id))
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

        // POST: api/PHANQUYENTUYENTHUs
        [ResponseType(typeof(PHANQUYENTUYENTHU))]
        public async Task<IHttpActionResult> PostPHANQUYENTUYENTHU(PHANQUYENTUYENTHU pHANQUYENTUYENTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PHANQUYENTUYENTHUs.Add(pHANQUYENTUYENTHU);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pHANQUYENTUYENTHU.IDPHANQUYENTUYENTHU }, pHANQUYENTUYENTHU);
        }

        // DELETE: api/PHANQUYENTUYENTHUs/5
        [ResponseType(typeof(PHANQUYENTUYENTHU))]
        public async Task<IHttpActionResult> DeletePHANQUYENTUYENTHU(int id)
        {
            PHANQUYENTUYENTHU pHANQUYENTUYENTHU = await db.PHANQUYENTUYENTHUs.FindAsync(id);
            if (pHANQUYENTUYENTHU == null)
            {
                return NotFound();
            }

            db.PHANQUYENTUYENTHUs.Remove(pHANQUYENTUYENTHU);
            await db.SaveChangesAsync();

            return Ok(pHANQUYENTUYENTHU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PHANQUYENTUYENTHUExists(int id)
        {
            return db.PHANQUYENTUYENTHUs.Count(e => e.IDPHANQUYENTUYENTHU == id) > 0;
        }
    }
}