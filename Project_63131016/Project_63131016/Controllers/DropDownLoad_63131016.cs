using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63131016.Models;
using System.Collections;
namespace Project_63131016.Controllers
{
    public class DropDownLoad_63131016Controller : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            using (Project_63131016Entities db = new Project_63131016Entities())
            {
                var loaisach = db.LoaiSaches.ToList();
                Hashtable tenloaisach = new Hashtable();
                foreach (var item in loaisach)
                {
                    tenloaisach.Add(item.MaLoaiSach, item.TenLoaiSach);
                }
                ViewBag.TenLoaiSach = tenloaisach;
                return PartialView("Index");
            }

        }
    }
}