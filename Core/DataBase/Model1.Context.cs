﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KinolistEntities : DbContext
    {
        public KinolistEntities()
            : base("name=KinolistEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Collection> Collection { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Film_Collection> Film_Collection { get; set; }
        public virtual DbSet<Follow> Follow { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
