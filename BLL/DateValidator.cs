using FluentValidation;

namespace BLL
{
    public class DateValidator : AbstractValidator<DietPlan>
    {
        public DateValidator()
        {
            RuleFor(m => m.EndDate)
                .GreaterThan(m => m.StartDate.Date)
                .WithMessage("End date must follow Start date");
        }
    }
}