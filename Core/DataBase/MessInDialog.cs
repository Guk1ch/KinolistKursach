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
    
    public partial class MessInDialog
    {
        public int ID { get; set; }
        public Nullable<int> IdMessage { get; set; }
        public Nullable<int> IdFollow { get; set; }
    
        public virtual Follow Follow { get; set; }
        public virtual Message Message { get; set; }
    }
}