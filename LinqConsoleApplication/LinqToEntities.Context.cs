﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinqConsoleApplication
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LinqEntities : DbContext
    {
        public LinqEntities()
            : base("name=LinqEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<airplanedestination> airplanedestinations { get; set; }
        public DbSet<booking> bookings { get; set; }
        public DbSet<costumer> costumers { get; set; }
        public DbSet<destination> destinations { get; set; }
        public DbSet<vacationhouse> vacationhouses { get; set; }
        public DbSet<vacationweek> vacationweeks { get; set; }
    }
}
