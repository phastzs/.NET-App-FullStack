﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThiGK63CNTT2_63131016.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ThiGK63CNTT2_63131016Entities : DbContext
    {
        public ThiGK63CNTT2_63131016Entities()
            : base("name=ThiGK63CNTT2_63131016Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<THANHVIEN> THANHVIENs { get; set; }
        public virtual DbSet<TINH> TINHs { get; set; }
    
        public virtual int ThanhVien_TimKiem(string maTV, string maTinh)
        {
            var maTVParameter = maTV != null ?
                new ObjectParameter("MaTV", maTV) :
                new ObjectParameter("MaTV", typeof(string));
    
            var maTinhParameter = maTinh != null ?
                new ObjectParameter("MaTinh", maTinh) :
                new ObjectParameter("MaTinh", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ThanhVien_TimKiem", maTVParameter, maTinhParameter);
        }
    }
}
