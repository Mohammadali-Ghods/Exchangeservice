using Domain.Models;
using FluentValidation.Results;

namespace Domain.ResultModel
{
    public class Result
    {
        public bool SucceedResult { get; set; }
        public  ValidationResult FailedResults { get; set; }
    }

  
}
