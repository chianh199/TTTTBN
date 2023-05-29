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
    public class KYTHUsController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        //lay da ky thu
        // GET: api/KYTHUs
        public IQueryable<KYTHU> GetKYTHUs()
        {
            return db.KYTHUs;
        }

        // GET: api/KYTHUs/5
        [ResponseType(typeof(KYTHU))]
        public async Task<IHttpActionResult> GetKYTHU(int id)
        {
            KYTHU kYTHU = await db.KYTHUs.FindAsync(id);
            if (kYTHU == null)
            {
                return NotFound();
            }

            return Ok(kYTHU);
        }

        // PUT: api/KYTHUs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutKYTHU(int id, KYTHU kYTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kYTHU.IDKYTHU)
            {
                return BadRequest();
            }

            db.Entry(kYTHU).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KYTHUExists(id))
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

        // POST: api/KYTHUs
        [ResponseType(typeof(KYTHU))]
        public async Task<IHttpActionResult> PostKYTHU(KYTHU kYTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KYTHUs.Add(kYTHU);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = kYTHU.IDKYTHU }, kYTHU);
        }

        // DELETE: api/KYTHUs/5
        [ResponseType(typeof(KYTHU))]
        public async Task<IHttpActionResult> DeleteKYTHU(int id)
        {
            KYTHU kYTHU = await db.KYTHUs.FindAsync(id);
            if (kYTHU == null)
            {
                return NotFound();
            }

            db.KYTHUs.Remove(kYTHU);
            await db.SaveChangesAsync();

            return Ok(kYTHU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KYTHUExists(int id)
        {
            return db.KYTHUs.Count(e => e.IDKYTHU == id) > 0;
        }
    }
}