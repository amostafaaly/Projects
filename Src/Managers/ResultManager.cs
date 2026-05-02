using Projects.Src.Contracts.IManger;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Managers
{
    public class ResultManager : GenericManager<Result>, IResultManager
    {
        public void Clear()
        {
             var results=GetAll().ToList();
            results.Clear();
        }

        public List<Result> GetResultByStudentid(string studentid)
        {
            var result=GetAll().Where(s=>s.StudentId==studentid).ToList();
            return result;
        }
     

        
    }
}
