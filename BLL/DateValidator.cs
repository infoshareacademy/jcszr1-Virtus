using FluentValidation;

namespace BLL
{
    public class DateValidator : AbstractValidator<DietPlan>
    {
        public DateValidator()
        {
            RuleFor(m => m.EndDate)
                .GreaterThanOrEqualTo(m => m.StartDate.Date)
                .WithMessage("End date cannot be prior to Start date.");
        }
    }
}