using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Contracts
{
    public interface IresultService
    {
        public void AddResult(Student student, Exam exam, int score);
        public void GetResultsByStudent(string studentid);
    }
}