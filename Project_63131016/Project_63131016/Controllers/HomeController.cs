using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Project_63131016.Models;
using PagedList;
namespace Project_63131016.Controllers
{
    public class HomeController : Controller
    {
        Project_63131016Entities db = new Project_63131016Entities();
        public ActionResult Index(string currentFilter, int?page, int maloaisach = 0, string SearchString = "")
        {
            if (SearchString != "")
            {
                page = 1;
                var sanPhams = db.Saches.Include(s => s.LoaiSach).Where(x => x.TenSach.ToUpper().Contains(SearchString.ToUpper()));
                sanPhams = sanPhams.OrderBy(x => x.TenSach);
                int pageSize = sanPhams.Count();
                int pageNumber = (page ?? 1);
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
            else
                SearchString = currentFilter;
            ViewBag.CurrentFilter = currentFilter;
            if (maloaisach == 0)
            {
                int pageSize = 12;
                int pageNumber = (page ?? 1);
                var sanPhams = db.Saches.Include(s => s.LoaiSach).OrderBy(x => x.TenSach);
                //phải order trước skip
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
            else//lọc theo loại sản phẩm
            {
                var Saches = db.Saches.Include(s => s.LoaiSach).Where(x=>x.MaLoaiSach==maloaisach);
                Saches = Saches.OrderBy(x => x.TenSach);
                int pageSize = Saches.Count();
                int pageNumber = (page ?? 1);
                return View(Saches.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}