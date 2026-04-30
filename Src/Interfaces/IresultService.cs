using Projects.Src.Models;
using Projects.Src.Models.Courses;
using Projects.Src.Models.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Interfaces
{
    public interface IresultService
    {
        public void AddResult(Student student, Exam exam, int score);
        public void GetResultsByStudent(Student student);
    }
}