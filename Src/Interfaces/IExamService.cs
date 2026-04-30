using Projects.Src.Models;
using Projects.Src.Models.Courses;
using Projects.Src.Models.Instructors;
using Projects.Src.Models.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Interfaces
{
    public interface IExamService
    {
        public void MakeExam(Instructor instructor, Exam exam);
        public void AssignToExam(Exam exam, Student student);
    }
}