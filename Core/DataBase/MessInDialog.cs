//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
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
