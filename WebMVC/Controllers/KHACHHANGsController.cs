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
    public class KHACHHANGsController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        // GET: api/KHACHHANGs
        public IEnumerable<KHACHHANG> Get()
        {

            List<KHACHHANG> kh = db.KHACHHANGs.ToList();
            //List<LOAIKH> ctpq = db.LOAIKHs.ToList();
            foreach (KHACHHANG kh1 in kh)
            {
                //List<LOAIKH> lkh = ctpq.FindAll(m => m.IDLOAIKH == kh1.IDLOAIKH);

                LOAIKH lkh = db.LOAIKHs.FirstOrDefault(x => x.IDLOAIKH == kh1.IDLOAIKH);
                kh1.LOAIKH = lkh;
                //kh1.LOAIKH.TENLOAIPHI = lkh.TENLOAIPHI;
                kh1.LOAIKH.KHACHHANGs = null;
                //foreach (LOAIKH ct in lkh)
                //{
                //    kh1.LOAIKH = ct;
                //}

            }
            foreach (KHACHHANG kh1 in kh)
            {
                TUYENTHU lkh = db.TUYENTHUs.FirstOrDefault(x => x.IDTUYENTHU == kh1.IDTUYENTHU);
                kh1.TUYENTHU = lkh;
                QUANHUYEN qh = db.QUANHUYENs.FirstOrDefault(x => x.IDQUANHUYEN == lkh.IDQUANHUYEN);
                lkh.QUANHUYEN = qh;
                kh1.TUYENTHU.KHACHHANGs = null;
                //kh1 = null;

            }

            return kh;
        }

        // GET: api/KHACHHANGs/5
        [ResponseType(typeof(KHACHHANG))]
        public async Task<IHttpActionResult> GetKHACHHANG(int id)
        {
            KHACHHANG kHACHHANG = await db.KHACHHANGs.FindAsync(id);
            if (kHACHHANG == null)
            {
                return NotFound();
            }
                //rep toi LOAIKH
                LOAIKH lkh = db.LOAIKHs.FirstOrDefault(x => x.IDLOAIKH == kHACHHANG.IDLOAIKH);
                kHACHHANG.LOAIKH = lkh;
                //rep toi TUYENTHU
                TUYENTHU tt = db.TUYENTHUs.FirstOrDefault(x => x.IDTUYENTHU == kHACHHANG.IDTUYENTHU);
                kHACHHANG.TUYENTHU = tt;
                QUANHUYEN qh = db.QUANHUYENs.FirstOrDefault(x => x.IDQUANHUYEN == tt.IDQUANHUYEN);
                tt.QUANHUYEN = qh;
                //kh1 = null;

            
            return Ok(kHACHHANG);
        }

        // PUT: api/KHACHHANGs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutKHACHHANG(int id, KHACHHANG kHACHHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kHACHHANG.IDKHACHHANG)
            {
                return BadRequest();
            }

            db.Entry(kHACHHANG).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KHACHHANGExists(id))
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

        // POST: api/KHACHHANGs
        [ResponseType(typeof(KHACHHANG))]
        public async Task<IHttpActionResult> PostKHACHHANG(KHACHHANG kHACHHANG)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KHACHHANGs.Add(kHACHHANG);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = kHACHHANG.IDKHACHHANG }, kHACHHANG);
        }

        // DELETE: api/KHACHHANGs/5
        [ResponseType(typeof(KHACHHANG))]
        public async Task<IHttpActionResult> DeleteKHACHHANG(int id)
        {
            KHACHHANG kHACHHANG = await db.KHACHHANGs.FindAsync(id);
            if (kHACHHANG == null)
            {
                return NotFound();
            }

            db.KHACHHANGs.Remove(kHACHHANG);
            await db.SaveChangesAsync();

            return Ok(kHACHHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KHACHHANGExists(int id)
        {
            return db.KHACHHANGs.Count(e => e.IDKHACHHANG == id) > 0;
        }
    }
}