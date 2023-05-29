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
    public class THUNGANsController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        // GET: api/THUNGANs
        public IQueryable<NHANVIEN> GetNHANVIENs()
        {
            return db.NHANVIENs;
        }

        // GET: api/THUNGANs/5
        [Route("kh/{idNhanvien}")]
        public List<KHACHHANG> Get(int idNhanvien)
        {
            TTTT3Entities1 db = new TTTT3Entities1();
            List<PHANQUYENTUYENTHU> lpqtt = db.PHANQUYENTUYENTHUs.ToList();
            List<PHANQUYENTUYENTHU> lpqtt1 = new List<PHANQUYENTUYENTHU>();
            foreach (PHANQUYENTUYENTHU l in lpqtt)
            {
                if (idNhanvien == l.IDNHANVIEN)
                {
                    lpqtt1.Add(l);
                }
            }
            List<KHACHHANG> lkh = db.KHACHHANGs.ToList();
            List<KHACHHANG> lkh1 = new List<KHACHHANG>();
            foreach (PHANQUYENTUYENTHU l in lpqtt1)
            {
                foreach (KHACHHANG k in lkh)
                {
                    if (k.IDTUYENTHU == l.IDTUYENTHU)
                    {
                        lkh1.Add(k);
                    }
                }
            }
            return lkh1;
        }
        //Nut xac nhan phieu thu
        // PUT: api/THUNGANs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNHANVIEN(int id, NHANVIEN nHANVIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nHANVIEN.IDNHANVIEN)
            {
                return BadRequest();
            }

            db.Entry(nHANVIEN).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NHANVIENExists(id))
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

        // POST: api/THUNGANs
        [ResponseType(typeof(NHANVIEN))]
        public async Task<IHttpActionResult> PostNHANVIEN(NHANVIEN nHANVIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NHANVIENs.Add(nHANVIEN);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nHANVIEN.IDNHANVIEN }, nHANVIEN);
        }

        // DELETE: api/THUNGANs/5
        [ResponseType(typeof(NHANVIEN))]
        public async Task<IHttpActionResult> DeleteNHANVIEN(int id)
        {
            NHANVIEN nHANVIEN = await db.NHANVIENs.FindAsync(id);
            if (nHANVIEN == null)
            {
                return NotFound();
            }

            db.NHANVIENs.Remove(nHANVIEN);
            await db.SaveChangesAsync();

            return Ok(nHANVIEN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NHANVIENExists(int id)
        {
            return db.NHANVIENs.Count(e => e.IDNHANVIEN == id) > 0;
        }
    }
}