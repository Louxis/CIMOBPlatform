//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class ApplicationStatHistory
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationStat { get; set; }
        public System.DateTime DateOfUpdate { get; set; }
    
        public virtual Application Application { get; set; }
    }
}
