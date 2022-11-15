using Domain.Commands.CustomerExchangeCommands.Base;
using FluentValidation;

namespace Domain.Commands.CustomerExchangeCommands.Validations
{
    public abstract class CustomerExchangeValidation<T> : AbstractValidator<T> where T : CustomerExchangeCommand
    {
        protected void ValidateID()
        {
            RuleFor(c => c.ID)
                .NotEmpty().WithMessage("Please ensure you have entered the TransactionID")
                .Length(36).WithMessage("The TransactionID length should be 36 characters not more");
        }
        protected void ValidateCustomerID()
        {
            RuleFor(c => c.CustomerID)
                 .NotEmpty().WithMessage("Please ensure you have entered the CustomerID")
                 .Length(36).WithMessage("The CustomerID length should be 36 characters not more");
        }
    }

}