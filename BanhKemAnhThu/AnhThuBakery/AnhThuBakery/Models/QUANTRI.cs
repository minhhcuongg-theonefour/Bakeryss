//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnhThuBakery.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QUANTRI
    {
        public QUANTRI()
        {
            this.TINTUCs = new HashSet<TINTUC>();
        }
    
        public int MaQT { get; set; }
        public string TenDN { get; set; }
        public string MatKhau { get; set; }
        public Nullable<int> MaNhom { get; set; }
    
        public virtual NHOMQUANTRI NHOMQUANTRI { get; set; }
        public virtual ICollection<TINTUC> TINTUCs { get; set; }
    }
}