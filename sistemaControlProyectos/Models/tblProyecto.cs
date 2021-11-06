//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sistemaControlProyectos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProyecto()
        {
            this.tblActividad = new HashSet<tblActividad>();
            this.tblArea = new HashSet<tblArea>();
            this.tblCuadernoObra = new HashSet<tblCuadernoObra>();
            this.tblProfesional_Proyecto = new HashSet<tblProfesional_Proyecto>();
            this.tblReunion = new HashSet<tblReunion>();
        }
    
        public int IDProyecto { get; set; }
        public string titProyecto { get; set; }
        public System.DateTime fechaIniPro { get; set; }
        public System.DateTime fechaFinPro { get; set; }
        public string descripcion { get; set; }
        public Nullable<bool> estado { get; set; }
        public string Ubicacion { get; set; }
        public string distrito { get; set; }
        public string departamento { get; set; }
        public string imagen { get; set; }
        public string seguimiento { get; set; }
        public int IDProfesional { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblActividad> tblActividad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblArea> tblArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCuadernoObra> tblCuadernoObra { get; set; }
        public virtual tblProfesional tblProfesional { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProfesional_Proyecto> tblProfesional_Proyecto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblReunion> tblReunion { get; set; }
    }
}
