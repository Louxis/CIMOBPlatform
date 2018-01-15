//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFDbTest
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Application()
        {
            this.ApplicationStatHistories = new ObservableCollection<ApplicationStatHistory>();
            this.Documents = new ObservableCollection<Document>();
        }
    
        public int ApplicationId { get; set; }
        public int ApplicationStatId { get; set; }
        public Nullable<double> ArithmeticMean { get; set; }
        public Nullable<int> BilateralProtocol1Id { get; set; }
        public Nullable<int> BilateralProtocol2Id { get; set; }
        public Nullable<int> BilateralProtocol3Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<int> ECTS { get; set; }
        public string EmployeeId { get; set; }
        public Nullable<double> Enterview { get; set; }
        public Nullable<double> FinalGrade { get; set; }
        public Nullable<double> MotivationLetter { get; set; }
        public string Motivations { get; set; }
        public string StudentId { get; set; }
    
        public virtual ApplicationStat ApplicationStat { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual BilateralProtocol BilateralProtocol { get; set; }
        public virtual BilateralProtocol BilateralProtocol1 { get; set; }
        public virtual BilateralProtocol BilateralProtocol2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<ApplicationStatHistory> ApplicationStatHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Document> Documents { get; set; }
    }
}
