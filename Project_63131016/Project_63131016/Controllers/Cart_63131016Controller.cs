using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63131016.Models;
using System.Net;
using System.Net.Mail;
namespace Project_63131016.Controllers
{
    public class Cart_63131016Controller : Controller
    {
        private Project_63131016Entities db = new Project_63131016Entities();
        // GET: Giohang
        public ActionResult Index()
        {
            List<CartItem_63131016> giohang = Session["giohang"] as List<CartItem_63131016>;
            return View(giohang);
        }
        //khai báo phương thức thêm sản phẩm vào giỏ hàng
        public RedirectToRouteResult AddToCart(string MaSach)
        {
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<CartItem_63131016>();
            }
            List<CartItem_63131016> giohang = Session["giohang"] as List<CartItem_63131016>;
            //kiểm tra sản phẩm khách đang chọn có trong giỏ hàng chưa
            if (giohang.FirstOrDefault(m => m.MaSach == MaSach) == null)//chưa có trong giỏ hàng
            {
                Sach sach = db.Saches.Find(MaSach);
                CartItem_63131016 newItem = new CartItem_63131016();
                newItem.MaSach = MaSach;
                newItem.TenSach = sach.TenSach;
                newItem.SoLuong = 1;
                newItem.GiaBan = Convert.ToDouble(sach.GiaBan);
                giohang.Add(newItem);
            }
            else//sản phẩm đã có trong giỏ hàng ==>tăng số lượng lên 1
            {
                CartItem_63131016 cardItem = giohang.FirstOrDefault(m => m.MaSach == MaSach);
                cardItem.SoLuong++;
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Update(string MaSach, int txtSoluong)
        {
            //tìm CartItem muốn xóa
            List<CartItem_63131016> giohang = Session["giohang"] as List<CartItem_63131016>;
            CartItem_63131016 item = giohang.FirstOrDefault(m => m.MaSach == MaSach);
            if (item != null)
            {
                item.SoLuong = txtSoluong;
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult DelCartItem(string MaSach)
        {
            //tìm CartItem muốn xóa
            List<CartItem_63131016> giohang = Session["giohang"] as List<CartItem_63131016>;
            CartItem_63131016 item = giohang.FirstOrDefault(m => m.MaSach == MaSach);
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Order(string Email, string Phone)
        {
            List<CartItem_63131016> giohang = Session["giohang"] as List<CartItem_63131016>;
            string sMsg = "<html><body><table border='1'><caption>Thông tin đặt hàng</caption><tr><th>STT</th><th>Tên hàng</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr>";
            int i = 0;
            double tongtien = 0;
            foreach (CartItem_63131016 item in giohang)
            {
                i++;
                sMsg += "<tr>";
                sMsg += "<td>" + i.ToString() + "</td>";
                sMsg += "<td>" + item.TenSach + "</td>";
                sMsg += "<td>" + item.SoLuong.ToString() + "</td>";
                sMsg += "<td>" + item.GiaBan.ToString() + "</td>";
                sMsg += "<td>" + String.Format("{0:#,###}", item.SoLuong * item.GiaBan) + "</td>";
                sMsg += "<tr>";
                tongtien += item.SoLuong * item.GiaBan;
            }
            sMsg += "<tr><th colspan='5'>Tổng cộng: "
                + String.Format("{0:#,### vnđ}", tongtien) + "</th></tr></table>";
            MailMessage mail = new MailMessage("taikhoannguoigoi@gmail.com", Email, "Thông tin đơn hàng", sMsg);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("taikhoannguoigoi", "matkhau");
            mail.IsBodyHtml = true;
            client.Send(mail);
            return RedirectToAction("Index", "Home");
            //gởi mail cho khách hàng

        }
    }
}