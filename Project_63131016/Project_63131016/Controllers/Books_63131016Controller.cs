using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63131016.Models;
using System.IO;
using PagedList;
namespace Project_63131016.Controllers
{
    public class Books_63131016Controller : Controller
    {
        private Project_63131016Entities db = new Project_63131016Entities();

        // GET: SanPhams
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SortByPrice = (sortOrder == "giaban" ? "giaban_desc" : "giaban");
            ViewBag.CurrentSort = sortOrder;
            var saches = db.Saches.Include(s => s.LoaiSach);
            switch (sortOrder)
            {
                case "ten_desc":
                    saches = saches.OrderByDescending(s => s.TenSach);
                    break;
                case "giaban_desc":
                    saches = saches.OrderByDescending(s => s.GiaBan);
                    break;
                case "giaban":
                    saches = saches.OrderBy(s => s.GiaBan);
                    break;
                default://mặc định sắp xếp theo tên sản phẩm
                    saches = saches.OrderBy(s => s.TenSach);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(saches.ToPagedList(pageNumber, pageSize));

        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSach = new SelectList(db.LoaiSaches, "MaLoaiSach", "TenLoaiSach");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,SoLuongTon,GiaBan,MaLoaiSach,HinhSach")] Sach sach,
            HttpPostedFileBase HinhSach)
        {
            if (ModelState.IsValid)
            {
                if (HinhSach != null && HinhSach.ContentLength > 0)
                {
                    string filename = Path.GetFileName(HinhSach.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    sach.HinhSach = "Images/" + filename;
                    HinhSach.SaveAs(path);
                }
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiS = new SelectList(db.LoaiSaches, "MaLoaiSach", "TenLoaiSach", sach.MaLoaiSach);
            return View(sach);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id, string imgPath)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSaches, "MaLoaiSach", "TenLoaiSach", sach.MaLoaiSach);
            return View(sach);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,SoLuongTon,GiaBan,MaLoaiSach,HinhSach")] Sach sach,
            HttpPostedFileBase HinhUpload, string HinhSach)
        {
            if (ModelState.IsValid)
            {
                if (HinhUpload != null && HinhUpload.ContentLength > 0)
                {
                    string filename = Path.GetFileName(HinhUpload.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    sach.HinhSach = "Images/" + filename;
                    HinhUpload.SaveAs(path);
                }
                else
                {
                    sach.HinhSach = HinhSach;//nếu không chọn hình mới thì giữ hình cũ
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSaches, "MaLoaiSach", "TenLoaiSach", sach.MaLoaiSach);
            return View(sach);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            //xóa file hình trong thư mục images
            System.IO.File.Delete(Server.MapPath("~/" + sach.HinhSach));
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
