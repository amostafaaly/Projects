using Projects.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Interfaces
{
    public interface IexamService
    {
        public void MakeExam(Instructor instructor, Exam exam);
        public void AssignToExam(Exam exam, Student student);
    }
}
