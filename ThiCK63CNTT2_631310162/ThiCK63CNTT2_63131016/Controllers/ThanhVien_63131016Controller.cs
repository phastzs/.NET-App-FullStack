﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;
using ThiCK63CNTT2_63131016.Models;

namespace ThiCK63CNTT2_63131016.Controllers
{
    public class ThiCK63CNTT2_63131016Controller : Controller
    {
        private ThiGK63CNTT2_63131016Entities db = new ThiGK63CNTT2_63131016Entities();

        public ActionResult GioiThieu_63131016()
        {
            return View();
        }
        string LayMaTV()
        {
            var maMax = db.THANHVIENs.ToList().Select(n => n.MaTV).Max();
            int maTV = int.Parse(maMax.Substring(2)) + 1;
            string TV = String.Concat("000", maTV.ToString());
            return "TV" + TV.Substring(maTV.ToString().Length - 1);
        }
        //Index
        public ActionResult Index(int? page)
        {
            int pageSize = 2; // Số lượng bản ghi trên mỗi trang
            int pageNumber = (page ?? 1);

            var thanhViens = db.THANHVIENs.Include(n => n.TINH).ToList(); // Thay YourModel bằng tên của model SinhVien

            var pagedTHANHVIENs = thanhViens.ToPagedList(pageNumber, pageSize);

            return View(pagedTHANHVIENs);
        }

        //Xem Chi Tiet
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANHVIEN thanhVien = db.THANHVIENs.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            return View(thanhVien);
        }
        //get: Them moi
        public ActionResult Create()
        {

            ViewBag.MaTV = LayMaTV();
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh");
            return View();
        }
        //post: Them moi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,GioiTinh,NgaySinh,AnhNV,DiaChi,Email,MaLop")] THANHVIEN thanhVien)
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
                thanhVien.MaTV = LayMaTV();
                thanhVien.AnhTV = postedFileName;
                db.THANHVIENs.Add(thanhVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPB = new SelectList(db.TINHs, "MaTinh", "TenTinh", thanhVien.MaTinh);
            return View(thanhVien);
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