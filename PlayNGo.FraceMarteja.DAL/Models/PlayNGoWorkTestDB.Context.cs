﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlayNGo.FraceMarteja.DAL.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PlayNGoDBEntities : DbContext
    {
        public PlayNGoDBEntities()
            : base("name=PlayNGoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Hand> Hands { get; set; }
        public virtual DbSet<PlayerGroup> PlayerGroups { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
    }
}