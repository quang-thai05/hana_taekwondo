﻿namespace StudentManagement.DTOs.Output
{
    public class DeadlineTutionOutput
    {
        public int StudentId { get; set; }

        public string FullName { get; set; }
        public DateTime NotificationTime { get; set; }
        public string DueDate { get; set; }
    }
}
