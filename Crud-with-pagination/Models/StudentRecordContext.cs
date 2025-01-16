using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crud_with_pagination.Models;

public partial class StudentRecordContext : DbContext
{
    public StudentRecordContext()
    {
    }

    public StudentRecordContext(DbContextOptions<StudentRecordContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblStudent> Students { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.ToTable("TblStudent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
