using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.DTOs.CourseDTOs
{
    public class EnrollmentDto
    {
        public string CourseId { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public double? RawScore { get; set; }

    }
}
