﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CottonPrompt.Infrastructure.Entities;

public partial class OrderReport
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Reason { get; set; }

    public Guid ReportedBy { get; set; }

    public DateTime ReportedOn { get; set; }

    public Guid? ResolvedBy { get; set; }

    public DateTime? ResolvedOn { get; set; }

    public virtual Order Order { get; set; }
}