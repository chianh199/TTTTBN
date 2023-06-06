﻿using System;
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
        //lay danh sach phieu thu theo nhan vien
        // GET: pt/1/1
        [Route("pt/{idkythu}/{idNhanvien}")]
        public List<PHIEUTHU> GetPhieuthu(int idkythu, int idNhanvien)
        {

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
                    // (k.IDTUYENTHU == l.IDTUYENTHU) && k.TRANGTHAI == true
                    if (k.IDTUYENTHU == l.IDTUYENTHU)
                    {
                        lkh1.Add(k);
                    }
                }
            }

            List<PHIEUTHU> lpt = db.PHIEUTHUs.ToList();
            List<PHIEUTHU> lpt1 = new List<PHIEUTHU>();
            KYTHU kt = db.KYTHUs.ToList().Find(s => s.IDKYTHU == idkythu);
            List<CHITIETPHIEUTHU> ctp = db.CHITIETPHIEUTHUs.ToList();
            // List<CHITIETPHIEUTHU> ctp1 = new List<CHITIETPHIEUTHU>();
            List<NHANVIEN> lnv = db.NHANVIENs.ToList();
            foreach (KHACHHANG kh in lkh1)
            {
                List<PHIEUTHU> lptt = new List<PHIEUTHU>();
                lptt = lpt.FindAll(s => s.IDKHACHHANG == kh.IDKHACHHANG);

                foreach (PHIEUTHU p in lptt)
                {
                    if (((p.IDKYTHU == idkythu) && kt.TRANGTHAIKYTHU == true))
                    {
                        // List<CHITIETPHIEUTHU> a =
                        p.CHITIETPHIEUTHUs = ctp.FindAll(c => c.IDPHIEU == p.IDPHIEU);
                        p.NHANVIEN = lnv.Find(c => c.IDNHANVIEN == idNhanvien);
                        p.NHANVIEN.PHANQUYENTUYENTHUs = null;
                        p.NHANVIEN.PHIEUTHUs = null;
                        lpt1.Add(p);
                        p.KYTHU.PHIEUTHUs = null;

                    }

                }
            }


            return lpt1;
        }

        //danh sach khach hang theo nhan vien thu
        // GET: kh/5
        [Route("kh/{idNhanvien}")]
        public List<KHACHHANG> Get(int idNhanvien)
        {

            List<PHANQUYENTUYENTHU> lpqtt = db.PHANQUYENTUYENTHUs.ToList();
            List<PHANQUYENTUYENTHU> lpqtt1 = new List<PHANQUYENTUYENTHU>();
            foreach (PHANQUYENTUYENTHU l in lpqtt)
            {
                if (idNhanvien == l.IDNHANVIEN)
                {
                    lpqtt1.Add(l);
                }
            }
            List<LOAIKH> llkh = db.LOAIKHs.ToList();
            List<TUYENTHU> ltt = db.TUYENTHUs.ToList();
            List<KHACHHANG> lkh = db.KHACHHANGs.ToList();
            List<XAPHUONG> lxp = db.XAPHUONGs.ToList();
            List<KHACHHANG> lkh1 = new List<KHACHHANG>();
            List<QUANHUYEN> lqh = db.QUANHUYENs.ToList();
            foreach (PHANQUYENTUYENTHU l in lpqtt1)
            {
                foreach (KHACHHANG k in lkh)
                {
                    if ((k.IDTUYENTHU == l.IDTUYENTHU) && k.TRANGTHAI == true)
                    {
                        k.LOAIKH = llkh.Find(c => c.IDLOAIKH == k.IDLOAIKH);
                        k.LOAIKH.KHACHHANGs = null;
                        k.TUYENTHU.KHACHHANGs = null;
                        k.TUYENTHU.PHANQUYENTUYENTHUs = null;
                        k.TUYENTHU.XAPHUONG = lxp.Find(c => c.IDXAPHUONG == k.TUYENTHU.IDXAPHUONG);
                        k.TUYENTHU.XAPHUONG.TUYENTHUs = null;
                        k.TUYENTHU.XAPHUONG.QUANHUYEN = lqh.Find(c => c.IDQUANHUYEN == k.TUYENTHU.XAPHUONG.IDQUANHUYEN);
                        k.TUYENTHU.XAPHUONG.QUANHUYEN.XAPHUONGs = null;
                        lkh1.Add(k);
                    }
                }
            }
            return lkh1;
        }

        //Nut xac nhan phieu thu da thu rui
        // PUT: api/THUNGANs
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNHANVIEN(PHIEUTHU idphieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PHIEUTHU pt1 = db.PHIEUTHUs.Where(x => x.IDPHIEU == idphieu.IDPHIEU).FirstOrDefault();
            if (pt1==null)
            {
                return NotFound();
            }
            if(pt1.TRANGTHAIPHIEU == true)
            {
                ModelState.AddModelError("phieu", "Phiếu đã được xác nhận!");
                return BadRequest(ModelState);
            }

                pt1.TRANGTHAIPHIEU = true;
                db.SaveChanges();

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