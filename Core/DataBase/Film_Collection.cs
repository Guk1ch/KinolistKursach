//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Film_Collection
    {
        public int ID { get; set; }
        public Nullable<int> ID_Film { get; set; }
        public Nullable<int> ID_Collection { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Collection Collection { get; set; }
        public virtual Film Film { get; set; }
    }
}
