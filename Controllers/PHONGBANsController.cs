﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNPMNC_Project.Models;

namespace CNPMNC_Project.Controllers
{
    public class PHONGBANsController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: PHONGBANs
        public ActionResult Index()
        {
            return View(db.PHONGBANs.ToList());
        }

        // GET: PHONGBANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            if (pHONGBAN == null)
            {
                return HttpNotFound();
            }

            List<NHANVIEN> nhanvien = db.NHANVIENs.ToList();
            List<PHONGBAN> phongban = db.PHONGBANs.ToList();
            List<CHUCVU> chucvu = db.CHUCVUs.ToList();
            var list = from n in nhanvien
                       join p in phongban
                        on n.MAPB equals p.MAPB
                       join cv in chucvu on n.MACV equals cv.MACV
                       where p.MAPB == id.Value
                       select new ChiTietPhongBan
                       {
                           TenCV = cv.TENCV,
                           MANV = n.MANV,
                           HOTENNV = n.HOTENNV,
                           NGAYSINH = n.NGAYSINH,
                           DIACHI = n.DIACHI,
                           GIOITINH = n.GIOITINH,
                           CMND = n.CMND,
                           QUEQUAN = n.QUEQUAN,
                           SDT = n.SDT,
                           DANTOC = n.DANTOC,
                           SOBH = n.SOBH
                       }
                       ;

            return View(list);
        }

        // GET: PHONGBANs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PHONGBANs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPB,TENPB,TRGPB")] PHONGBAN pHONGBAN)
        {
            if (ModelState.IsValid)
            {
                db.PHONGBANs.Add(pHONGBAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pHONGBAN);
        }

        // GET: PHONGBANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            if (pHONGBAN == null)
            {
                return HttpNotFound();
            }
            return View(pHONGBAN);
        }

        // POST: PHONGBANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPB,TENPB,TRGPB")] PHONGBAN pHONGBAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHONGBAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pHONGBAN);
        }

        // GET: PHONGBANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            if (pHONGBAN == null)
            {
                return HttpNotFound();
            }
            return View(pHONGBAN);
        }

        // POST: PHONGBANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHONGBAN pHONGBAN = db.PHONGBANs.Find(id);
            db.PHONGBANs.Remove(pHONGBAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
