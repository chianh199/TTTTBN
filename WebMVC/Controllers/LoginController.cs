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
        // Đổi mật khẩu, tên tài khoản
        // PATH: api/Login/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Patch(int id, NHANVIEN nHANVIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nHANVIEN.IDNHANVIEN)
            {
                return BadRequest();
            }

            if (!ktrachuoi(nHANVIEN.USERNAME))
            {
                ModelState.AddModelError("checkname", "Tên người dùng không được có dấu và khoảng cách!");
                return BadRequest(ModelState);
            }

            var name1 = db.NHANVIENs.Count(e => e.USERNAME.Equals(nHANVIEN.USERNAME) && (id != e.IDNHANVIEN));
            if (name1 > 0)
            {
                ModelState.AddModelError("username", "Tên tài khoản đã tồn tại!");
                return BadRequest(ModelState);
            }



            byte[] buffer = Encoding.UTF8.GetBytes(nHANVIEN.PASSWORD);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            nHANVIEN.PASSWORD = null;
            for (int i = 0; i < buffer.Length; i++)
            {
                nHANVIEN.PASSWORD += buffer[i].ToString("x1");
            }

            NHANVIEN nHANVIEN1 = db.NHANVIENs.Where(x => x.IDNHANVIEN == id).FirstOrDefault();
            nHANVIEN1.USERNAME = nHANVIEN.USERNAME;
            nHANVIEN1.PASSWORD = nHANVIEN.PASSWORD;

            try
            {
                db.SaveChanges();
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

            if (!ktrachuoi(nHANVIEN.USERNAME))
            {
                ModelState.AddModelError("checkname", "Tên người dùng không được có dấu và khoảng cách!");
                return BadRequest(ModelState);
            }

            if (!UserExists(nHANVIEN.USERNAME, nHANVIEN.PASSWORD))
            {
                ModelState.AddModelError("checkuser","Mật khẩu hoặc tên tài khoản không chính xác!");
                return BadRequest(ModelState);
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
        // kiem tra chuoi nhap vao
        public bool ktrachuoi(string username)
        {
            bool flag = true;
            foreach (char a in username)
            {
                int asciiValue = (int)a;
                if ((asciiValue >= 38 && asciiValue <= 57) || (asciiValue >= 65 && asciiValue <= 90) || (asciiValue >= 97 && asciiValue <= 122))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            if (flag == true)
                return true;
            else
                return false;
        }
        private bool UserExists(String name, String pass)
        {
            bool ext = false;

            var name1 = db.NHANVIENs.Count(x => x.USERNAME == name);

            NHANVIEN nv = db.NHANVIENs.FirstOrDefault(x => x.USERNAME == name);

            byte[] buffer = Encoding.UTF8.GetBytes(pass);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            pass = null;
            for (int i = 0; i < buffer.Length; i++)
            {
                pass += buffer[i].ToString("x1");
            }

            if (name1 > 0)
            {
                if(nv.PASSWORD == pass)
                {
                    ext = true;
                }
            }
            return ext;
        }
    }
}