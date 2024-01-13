﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Entities;

public partial class CottonPromptContext : DbContext
{
    public CottonPromptContext(DbContextOptions<CottonPromptContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDesign> OrderDesigns { get; set; }

    public virtual DbSet<OrderDesignBracket> OrderDesignBrackets { get; set; }

    public virtual DbSet<OrderDesignComment> OrderDesignComments { get; set; }

    public virtual DbSet<OrderImageReference> OrderImageReferences { get; set; }

    public virtual DbSet<OrderOutputSize> OrderOutputSizes { get; set; }

    public virtual DbSet<OrderPrintColor> OrderPrintColors { get; set; }

    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderId");

            entity.Property(e => e.ArtistStatus).HasMaxLength(50);
            entity.Property(e => e.CheckerStatus).HasMaxLength(50);
            entity.Property(e => e.Concept).IsRequired();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.OrderNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.DesignBracket).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DesignBracketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderDesignBrackets");

            entity.HasOne(d => d.OutputSize).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OutputSizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderOutputSizes");

            entity.HasOne(d => d.PrintColor).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PrintColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_OrderPrintColors");
        });

        modelBuilder.Entity<OrderDesign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderDesigns_Id");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDesigns)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDesigns_Orders");
        });

        modelBuilder.Entity<OrderDesignBracket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderDesignBracketId");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<OrderDesignComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderDesignComments_Id");

            entity.Property(e => e.Comment).IsRequired();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.OrderDesign).WithMany(p => p.OrderDesignComments)
                .HasForeignKey(d => d.OrderDesignId)
                .HasConstraintName("FK_OrderDesignComments_OrderDesigns");

            entity.HasOne(d => d.User).WithMany(p => p.OrderDesignComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_OrderDesignComments_Users");
        });

        modelBuilder.Entity<OrderImageReference>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.LineId }).HasName("PK_OrderImageReferences_OrderID_LineId");

            entity.Property(e => e.Url).IsRequired();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderImageReferences)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderImageReferences_Orders");
        });

        modelBuilder.Entity<OrderOutputSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderOutputSizes_Id");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<OrderPrintColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderPrintColors_Id");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<OrderStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderStatusHistory_Id");

            entity.ToTable("OrderStatusHistory");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderStatusHistory_Orders");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users_Id");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}