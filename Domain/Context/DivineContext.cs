using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Domain.Context
{
    public class DivineContext : DbContext
    {
        public DivineContext(DbContextOptions<DivineContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<UploadDocumentImage> UploadDocumentImages { get; set; }
        public DbSet<ServiceEngineer> ServiceEngineers { get; set; }
        public DbSet<ServiceQuotation> ServiceQuotations { get; set; }
        public DbSet<ServiceAttachment> ServiceAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users").HasKey(s => s.Id);
            modelBuilder.Entity<UploadDocumentImage>().ToTable("UploadDocumentImage").HasKey(s => s.Id);
            modelBuilder.Entity<ServiceEngineer>().ToTable("ServiceEngineer").HasKey(s => s.Id);
            modelBuilder.Entity<ServiceQuotation>().ToTable("ServiceQuotation").HasKey(s => s.Id);
            modelBuilder.Entity<ServiceAttachment>().ToTable("ServiceAttachment").HasKey(s => s.Id);

        }
    }
}
