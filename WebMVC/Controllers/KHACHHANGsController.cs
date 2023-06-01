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
        //lấy danh sach khach hang
        // GET: api/KHACHHANGs
        public List<KHACHHANG> Get()
        {
            TTTT3Entities1 db = new TTTT3Entities1();
            List<KHACHHANG> lkh = db.KHACHHANGs.ToList();
            List<TUYENTHU> tt = db.TUYENTHUs.ToList();
            List<QUANHUYEN> qh = db.QUANHUYENs.ToList();
            List<XAPHUONG> xp = db.XAPHUONGs.ToList();
            List<LOAIKH> llkh = db.LOAIKHs.ToList();
            for (int i = 0; i < lkh.Count; i++)
            {

                TUYENTHU ttt = tt.Find(s => s.IDTUYENTHU == lkh[i].IDTUYENTHU);
                LOAIKH lkhh = llkh.Find(s => s.IDLOAIKH == lkh[i].IDLOAIKH);
                lkh[i].LOAIKH = lkhh;
                lkh[i].LOAIKH.KHACHHANGs = null;
                lkh[i].TUYENTHU.IDTUYENTHU = ttt.IDTUYENTHU;
                lkh[i].TUYENTHU.TENTUYENTHU = ttt.TENTUYENTHU;
                lkh[i].TUYENTHU.MATUYENTHU = ttt.MATUYENTHU;
                lkh[i].TUYENTHU.KHACHHANGs = null;
                lkh[i].TUYENTHU.XAPHUONG.QUANHUYEN.XAPHUONGs = null;

            }

            return lkh;
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
            XAPHUONG qh = db.XAPHUONGs.FirstOrDefault(x => x.IDXAPHUONG == tt.IDXAPHUONG);
            tt.XAPHUONG = qh;
            QUANHUYEN rqh = db.QUANHUYENs.FirstOrDefault(x => x.IDQUANHUYEN == qh.IDQUANHUYEN);
            tt.XAPHUONG.QUANHUYEN = rqh;
            
            //List<XAPHUONG> xp = db.XAPHUONGs.Where(x => x.IDQUANHUYEN == qh.IDQUANHUYEN).ToList();
            //List<XAPHUONG> xp1 = xp.FindAll(x => x.IDXAPHUONG == kHACHHANG.IDXAPHUONG);
            //tt.QUANHUYEN.XAPHUONGs = xp1;
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

            var dem = db.KHACHHANGs.Count(e => e.MAKHACHHANG.Equals(kHACHHANG.MAKHACHHANG));
            if (dem > 0)
            {
                return BadRequest();
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