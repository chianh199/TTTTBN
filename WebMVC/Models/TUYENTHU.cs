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
    
    public partial class TUYENTHU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TUYENTHU()
        {
            this.KHACHHANGs = new HashSet<KHACHHANG>();
            this.PHANQUYENTUYENTHUs = new HashSet<PHANQUYENTUYENTHU>();
        }
    
        public int IDTUYENTHU { get; set; }
        public int IDXAPHUONG { get; set; }
        public string MATUYENTHU { get; set; }
        public string TENTUYENTHU { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHACHHANG> KHACHHANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHANQUYENTUYENTHU> PHANQUYENTUYENTHUs { get; set; }
        public virtual XAPHUONG XAPHUONG { get; set; }
    }
}
