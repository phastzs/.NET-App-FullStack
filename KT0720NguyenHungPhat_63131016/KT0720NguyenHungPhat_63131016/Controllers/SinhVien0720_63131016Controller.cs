using KT0720NguyenHungPhat_63131016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;

namespace KT0720NguyenHungPhat_63131016.Controllers
{
    public class SinhVien0720_63131016Controller : Controller
    {
        private KT0720_63131016Entities db = new KT0720_63131016Entities();
        // GET: SinhVien0720_63131016

        public ActionResult GioiThieu_63131016()
        {
            return View();
        }
        string LayMaSV()
        {
            var maMax = db.SinhViens.ToList().Select(n => n.MaSV).Max();
            int maSV = int.Parse(maMax.Substring(2)) + 1;
            string SV = String.Concat("000", maSV.ToString());
            return "SV" + SV.Substring(maSV.ToString().Length - 1);
        }
        //Index
        public ActionResult Index(int? page)
        {
            int pageSize = 2; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1);

            var sinhViens = db.SinhViens.Include(n => n.Lop).ToList(); // Thay YourModel bằng tên của model SinhVien

            var pagedSinhViens = sinhViens.ToPagedList(pageNumber, pageSize);

            return View(pagedSinhViens);
        }

        //Xem Chi Tiet
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhViens.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }
        //get: Them moi
        public ActionResult Create()
        {

            ViewBag.MaSV = LayMaSV();
            ViewBag.MaLop = new SelectList(db.Lops, "MaLop", "TenLop");
            return View();
        }
        //post: Them moi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,GioiTinh,NgaySinh,AnhNV,DiaChi,MaLop")] SinhVien sinhVien)
        {
            //System.Web.HttpPostedFileBase Avatar;
            var imgSV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgSV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgSV.SaveAs(path);

            if (ModelState.IsValid)
            {
                sinhVien.MaSV = LayMaSV();
                sinhVien.AnhSV = postedFileName;
                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPB = new SelectList(db.Lops, "MaLop", "TenLop", sinhVien.MaLop);
            return View(sinhVien);
        }
        public ActionResult TimKiem_63131016(string maSV = "", string hoTen = "")
        {
            ViewBag.maSV = maSV;
            ViewBag.hoTen = hoTen;
            var sinhViens = db.SinhViens.SqlQuery("SinhVien_TimKiem'" + maSV + "','" + hoTen + "'");
            if (sinhViens.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(sinhViens.ToList());    
        }
        public ActionResult PrintList()
        {
            var sinhViens = db.SinhViens.Include(n => n.Lop).OrderBy(n => n.TenSV);
            return PartialView(sinhViens.ToList());
        }

    }
}