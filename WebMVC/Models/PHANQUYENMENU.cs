//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHANQUYENMENU
    {
        public int IDPHANQUYENMENU { get; set; }
        public Nullable<int> IDQUYEN { get; set; }
        public Nullable<int> IDMENU { get; set; }
    
        public virtual MENU MENU { get; set; }
        public virtual QUYEN QUYEN { get; set; }
    }
}