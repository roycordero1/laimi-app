//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LAIMIStock.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CategoriasSuministros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoriasSuministros()
        {
            this.Suministros = new HashSet<Suministros>();
        }
    
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string imagenURL { get; set; }
        public int idCategoriaSuministro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Suministros> Suministros { get; set; }
    }
}