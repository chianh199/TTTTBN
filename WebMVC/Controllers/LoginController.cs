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
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class LoginController : ApiController
    {
        private TTTT3Entities1 db = new TTTT3Entities1();

        // GET: api/Login
        public IQueryable<NHANVIEN> GetNHANVIENs()
        {
            return db.NHANVIENs;
        }

        // GET: api/Login/5
        [ResponseType(typeof(NHANVIEN))]
        public async Task<IHttpActionResult> GetNHANVIEN(int id)
        {
            NHANVIEN nHANVIEN = await db.NHANVIENs.FindAsync(id);
            if (nHANVIEN == null)
            {
                return NotFound();
            }

            return Ok(nHANVIEN);
        }

        // PUT: api/Login/5
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
        //Dang nhap 
        // POST: api/Login
        [ResponseType(typeof(NHANVIEN))]
        public async Task<IHttpActionResult> PostNHANVIEN(NHANVIEN nHANVIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            if (!UserExists(nHANVIEN.USERNAME, nHANVIEN.PASSWORD))
            {
                return NotFound();
            }
            var usr = await db.NHANVIENs
                .Include(s => s.CHITIETPHANQUYENs)
                .FirstOrDefaultAsync(s => s.USERNAME.Equals(nHANVIEN.USERNAME));

            List<CHITIETPHANQUYEN> ct = db.CHITIETPHANQUYENs.Where(x => x.IDNHANVIEN == usr.IDNHANVIEN).ToList();
            foreach (CHITIETPHANQUYEN ct1 in ct)
            {
                QUYEN q = db.QUYENs.FirstOrDefault(x => x.IDQUYEN == ct1.IDQUYEN);
                ct1.QUYEN = q;
                ct1.NHANVIEN = null;
            }
                //int Numrd;
                //string Numrd_str;
            Random rd = new Random();
            //Numrd = rd.Next(1, 100);//biến Numrd sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 100
            //Numrd_str = rd.Next(1, 100).ToString();//Chuyển giá trị ramdon về kiểu string
            ModelLogin nv = new ModelLogin
            {
                IDNHANVIEN = usr.IDNHANVIEN,
                USERNAME = usr.USERNAME,
                PASSWORD = usr.PASSWORD,
                token = rd.Next(1, 100).ToString(),
                DIACHI = usr.DIACHI,
                SDT = usr.SDT,
                MANHANVIEN = usr.MANHANVIEN,
                HOTEN = usr.HOTEN,
                NGAYSINH = usr.NGAYSINH,
                CHITIETPHANQUYENs = ct
            };

            return Ok(nv);


        }

        // DELETE: api/Login/5
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
        private bool UserExists(String name, String pass)
        {
            bool ext = false;
            var name1 = db.NHANVIENs.Count(x => x.USERNAME == name);
            
            byte[] buffer = Encoding.UTF8.GetBytes(pass);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            pass = null;
            for (int i = 0; i < buffer.Length; i++)
            {
                pass += buffer[i].ToString("x1");
            }

            var pass1 = db.NHANVIENs.Count(x => x.PASSWORD.Equals(pass));
            if (name1 >0 && pass1 >0)
            {
                ext = true;
            }
            return ext;
        }
    }
}