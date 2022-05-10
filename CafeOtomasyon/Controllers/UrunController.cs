using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeOtomasyon.Models;

namespace CafeOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        private cafeotomasyonEntities db = new cafeotomasyonEntities();

        // GET: Urun
        public ActionResult Index()
        {
            var urun = db.Urun.Include(u => u.Kategori);
            return View(urun.ToList());
        }

        // GET: Urun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Urun/Create
        public ActionResult Create()
        {
            ViewBag.idKategori = new SelectList(db.Kategori, "id", "kategoriAd");
            return View();
        }

        // POST: Urun/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,urunAd,Fiyat,idKategori")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idKategori = new SelectList(db.Kategori, "id", "kategoriAd", urun.idKategori);
            return View(urun);
        }

        // GET: Urun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.idKategori = new SelectList(db.Kategori, "id", "kategoriAd", urun.idKategori);
            return View(urun);
        }

        // POST: Urun/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,urunAd,Fiyat,idKategori")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idKategori = new SelectList(db.Kategori, "id", "kategoriAd", urun.idKategori);
            return View(urun);
        }

        // GET: Urun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Find(id);
            db.Urun.Remove(urun);
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
