﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CottonPrompt.Infrastructure.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string OrderNumber { get; set; }

    public bool Priority { get; set; }

    public string Concept { get; set; }

    public int PrintColorId { get; set; }

    public int DesignBracketId { get; set; }

    public int OutputSizeId { get; set; }

    public string CustomerEmail { get; set; }

    public string CustomerStatus { get; set; }

    public Guid? ArtistId { get; set; }

    public string ArtistStatus { get; set; }

    public Guid? CheckerId { get; set; }

    public string CheckerStatus { get; set; }

    public Guid? ChangeRequestArtistId { get; set; }

    public string ChangeRequestArtistStatus { get; set; }

    public string ChangeRequestCheckerStatus { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual OrderDesignBracket DesignBracket { get; set; }

    public virtual ICollection<OrderDesign> OrderDesigns { get; set; } = new List<OrderDesign>();

    public virtual ICollection<OrderImageReference> OrderImageReferences { get; set; } = new List<OrderImageReference>();

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    public virtual OrderOutputSize OutputSize { get; set; }

    public virtual OrderPrintColor PrintColor { get; set; }
}