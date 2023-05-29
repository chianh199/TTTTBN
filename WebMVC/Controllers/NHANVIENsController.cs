using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class NHANVIENsController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        // GET: api/NHANVIENs
        public IEnumerable<NHANVIEN> Get()
        {

            List<NHANVIEN> nv = db.NHANVIENs.ToList();
            List<CHITIETPHANQUYEN> ctpq = db.CHITIETPHANQUYENs.ToList();
            foreach (NHANVIEN nv2 in nv)
            {
                List<CHITIETPHANQUYEN> ctpq1 = ctpq.FindAll(m => m.IDNHANVIEN == nv2.IDNHANVIEN);
                foreach (CHITIETPHANQUYEN ct in ctpq1)
                {
                    nv2.CHITIETPHANQUYENs.Add(ct);
                    QUYEN q = db.QUYENs.FirstOrDefault(x => x.IDQUYEN == ct.IDQUYEN);
                    ct.QUYEN = q;
                    ct.QUYEN.CHITIETPHANQUYENs = null;
                }
            }


            return nv;
        }

        // GET: api/NHANVIENs/5
        [ResponseType(typeof(NHANVIEN))]
        public async Task<IHttpActionResult> GetNHANVIEN(int id)
        {
            var nHANVIEN = await db.NHANVIENs
                .Include(s => s.CHITIETPHANQUYENs)
                .FirstOrDefaultAsync(s => s.IDNHANVIEN == id);

            if (nHANVIEN == null)
            {
                return NotFound();
            }

            List<CHITIETPHANQUYEN> ctpq = db.CHITIETPHANQUYENs.ToList();

            List<CHITIETPHANQUYEN> ctpq1 = ctpq.FindAll(m => m.IDNHANVIEN == nHANVIEN.IDNHANVIEN);
            foreach (CHITIETPHANQUYEN ct in ctpq1)
            {
                nHANVIEN.CHITIETPHANQUYENs.Add(ct);
                QUYEN q = db.QUYENs.FirstOrDefault(x => x.IDQUYEN == ct.IDQUYEN);
                ct.QUYEN = q;
                ct.QUYEN.CHITIETPHANQUYENs = null;
            }

            //NHANVIENDTO nHANVIENFiltered = new NHANVIENDTO
            //{
            //  IDNHANVIEN = nHANVIEN.IDNHANVIEN,
            //MANHANVIEN = nHANVIEN.MANHANVIEN,
            // HOTEN = nHANVIEN.HOTEN,
            //SDT = nHANVIEN.SDT,
            // CHITIETPHANQUYENs = nHANVIEN.CHITIETPHANQUYENs.Select(c => new CHITIETPHANQUYENDTO
            // {
            //   IDQUYEN = c.QUYEN.IDQUYEN,
            //   TENQUYEN = c.QUYEN.TENQUYEN
            //}).ToList()
            //};

            return Ok(nHANVIEN);


        }


        // PUT: api/NHANVIENs/5
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
                byte[] buffer = Encoding.UTF8.GetBytes(nHANVIEN.PASSWORD);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                buffer = md5.ComputeHash(buffer);
                nHANVIEN.PASSWORD = null;
                for (int i = 0; i < buffer.Length; i++)
                {
                    nHANVIEN.PASSWORD += buffer[i].ToString("x1");
                }
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

        // POST: api/NHANVIENs
        [ResponseType(typeof(NHANVIEN))]
        public async Task<IHttpActionResult> PostNHANVIEN(NHANVIEN nHANVIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var name1 = db.NHANVIENs.Count(e => e.USERNAME.Equals(nHANVIEN.USERNAME));
            if (name1 > 0)
            {
                return BadRequest();
            }

            byte[] buffer = Encoding.UTF8.GetBytes(nHANVIEN.PASSWORD);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            nHANVIEN.PASSWORD = null;
            for (int i = 0; i < buffer.Length; i++)
            {
                nHANVIEN.PASSWORD += buffer[i].ToString("x1");
            }

            //nHANVIEN.PASSWORD = BCrypt.Net.BCrypt.HashPassword(nHANVIEN.PASSWORD);

            db.NHANVIENs.Add(nHANVIEN);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = nHANVIEN.IDNHANVIEN }, nHANVIEN);
        }

        // DELETE: api/NHANVIENs/5
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