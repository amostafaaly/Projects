using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Contracts.Iservice
{
   public interface IcourseService
    {
        public Course CreateCourse(Faculty faculty, string name, int creditHours, string? description = null);
        public void AssignInstructor(string courseId, string instructorId);
    }
}
