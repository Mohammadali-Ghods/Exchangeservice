using FluentValidation.Results;

namespace Application.ViewModel
{
    public class ResultModel
    {
        public CustomerExchangeViewModel SucceedResult { get; set; }
        public ValidationResult FailedResults { get; set; }
    }
}
