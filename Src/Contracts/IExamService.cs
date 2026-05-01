using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Contracts
{
    public  interface IExamService
    {
        public void MakeExam(Instructor instructor, Exam exam);
        public void AssignToExam(Exam exam, Student student);
    }
}
