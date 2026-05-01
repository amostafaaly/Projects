using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Contracts.Iservice
{
    public interface IresultService
    {
        public Result AddResult(Student student, Exam exam, int score);
        public void GetResultsByStudent(string studentid);
    }
}