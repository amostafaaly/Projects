using Projects.Src.DTOs.ResultDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Interfaces
{
    public interface IResultService
    {
        void AddResult(CreateResultDto dto);
        IEnumerable<UpdateResultDto> GetAllResults();
        IEnumerable<UpdateResultDto> GetResultsByStudent(string studentId);
        void UpdateResult(string studentId, string examId, int newScore);
        void DeleteResult(string studentId, string examId);
    }
}
