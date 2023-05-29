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
    public class PHIEUTHUsController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        // GET: api/PHIEUTHUs
        public IEnumerable<PHIEUTHU> GetPHIEUTHUs()
        {
            List<PHIEUTHU> pt = db.PHIEUTHUs.ToList();
            foreach(PHIEUTHU pt1 in pt)
            {
                KHACHHANG kh = db.KHACHHANGs.Where(x => x.IDKHACHHANG == pt1.IDKHACHHANG).FirstOrDefault();
                KYTHU kt = db.KYTHUs.Where(x => x.IDKYTHU == pt1.IDKYTHU).FirstOrDefault();
                NHANVIEN nv = db.NHANVIENs.Where(x => x.IDNHANVIEN == pt1.IDNHANVIEN).FirstOrDefault();
                pt1.KHACHHANG = kh;
                pt1.KYTHU = kt;
                pt1.NHANVIEN = nv;
            }
            return pt;
        }

        // GET: api/PHIEUTHUs/5
        [ResponseType(typeof(PHIEUTHU))]
        public async Task<IHttpActionResult> GetPHIEUTHU(int id)
        {
            PHIEUTHU pt1 = await db.PHIEUTHUs.FindAsync(id);
            if (pt1 == null)
            {
                return NotFound();
            }
            KHACHHANG kh = db.KHACHHANGs.Where(x => x.IDKHACHHANG == pt1.IDKHACHHANG).FirstOrDefault();
            KYTHU kt = db.KYTHUs.Where(x => x.IDKYTHU == pt1.IDKYTHU).FirstOrDefault();
            NHANVIEN nv = db.NHANVIENs.Where(x => x.IDNHANVIEN == pt1.IDNHANVIEN).FirstOrDefault();
            pt1.KHACHHANG = kh;
            pt1.KYTHU = kt;
            pt1.NHANVIEN = nv;

            return Ok(pt1);
        }

        // PUT: api/PHIEUTHUs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPHIEUTHU(int id, PHIEUTHU pHIEUTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pHIEUTHU.IDPHIEU)
            {
                return BadRequest();
            }

            db.Entry(pHIEUTHU).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PHIEUTHUExists(id))
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

        // POST: api/PHIEUTHUs
        [ResponseType(typeof(PHIEUTHU))]
        public async Task<IHttpActionResult> PostPHIEUTHU(PHIEUTHU pHIEUTHU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PHIEUTHUs.Add(pHIEUTHU);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pHIEUTHU.IDPHIEU }, pHIEUTHU);
        }

        // DELETE: api/PHIEUTHUs/5
        [ResponseType(typeof(PHIEUTHU))]
        public async Task<IHttpActionResult> DeletePHIEUTHU(int id)
        {
            PHIEUTHU pHIEUTHU = await db.PHIEUTHUs.FindAsync(id);
            if (pHIEUTHU == null)
            {
                return NotFound();
            }

            db.PHIEUTHUs.Remove(pHIEUTHU);
            await db.SaveChangesAsync();

            return Ok(pHIEUTHU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PHIEUTHUExists(int id)
        {
            return db.PHIEUTHUs.Count(e => e.IDPHIEU == id) > 0;
        }
    }
}