﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KinolistFinalEntities : DbContext
    {
        public KinolistFinalEntities()
            : base("name=KinolistFinalEntities")
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
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<MessInDialog> MessInDialog { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
