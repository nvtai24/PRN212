﻿using System;
using System.Collections.Generic;

namespace PRN212.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int UserId { get; set; }

    public string Message { get; set; } = null!;

    public string? PlateNumber { get; set; }

    public DateTime? SentDate { get; set; }

    public bool? IsRead { get; set; }

    public virtual Vehicle? PlateNumberNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
