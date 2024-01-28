using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63131016.Models
{
    [Serializable]
    public class CartItem_63131016
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien
        {
            get { return SoLuong * GiaBan; }
        }
    }
}