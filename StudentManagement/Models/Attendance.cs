﻿using System;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public partial class Attendance
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public int StudentId { get; set; }
        public bool IsAttendance { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }

        public virtual Slot Slot { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
