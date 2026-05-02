using System;
using System.Collections.Generic;
using System.Linq;
using Projects.Src.Contracts.IManger;
using Projects.Src.Models;

namespace Projects.Src.Managers
{
    public class CourseManager : GenericManager<Course>, ICourseManager
    {
        public string GenerateCourseCode(Faculty faculty)
        {
            var facultyName = faculty.ToString();
            var prefix = facultyName.Substring(0, 2).ToUpper();
            var code = Random.Shared.Next(100, 500);
            return $"{prefix}-{code}";
        }

        
    }
}


