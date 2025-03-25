﻿using System;
using System.Collections.Generic;

namespace PRN212.Models;

public partial class Violation
{
    public int ViolationId { get; set; }

    public int ReportId { get; set; }

    public string PlateNumber { get; set; } = null!;

    public int? ViolatorId { get; set; }

    public decimal FineAmount { get; set; }

    public DateTime? FineDate { get; set; }

    public bool? PaidStatus { get; set; }

    public virtual Vehicle PlateNumberNavigation { get; set; } = null!;

    public virtual Report Report { get; set; } = null!;

    public virtual User? Violator { get; set; }
    
}
