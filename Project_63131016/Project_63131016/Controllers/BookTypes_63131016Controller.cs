using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63131016.Models;

namespace Project_63131016.Controllers
{
    public class BookTypes_63131016Controller : Controller
    {
        private Project_63131016Entities db = new Project_63131016Entities();

        // GET: BookTypes_63131016
        public ActionResult Index()
        {
            return View(db.LoaiSaches.ToList());
        }

        // GET: BookTypes_63131016/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            if (loaiSach == null)
            {
                return HttpNotFound();
            }
            return View(loaiSach);
        }

        // GET: BookTypes_63131016/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookTypes_63131016/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiSach,TenLoaiSach")] LoaiSach loaiSach)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSaches.Add(loaiSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSach);
        }

        // GET: BookTypes_63131016/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            if (loaiSach == null)
            {
                return HttpNotFound();
            }
            return View(loaiSach);
        }

        // POST: BookTypes_63131016/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiSach,TenLoaiSach")] LoaiSach loaiSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSach);
        }

        // GET: BookTypes_63131016/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            if (loaiSach == null)
            {
                return HttpNotFound();
            }
            return View(loaiSach);
        }

        // POST: BookTypes_63131016/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSach loaiSach = db.LoaiSaches.Find(id);
            db.LoaiSaches.Remove(loaiSach);
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
