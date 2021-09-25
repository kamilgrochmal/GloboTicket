using FluentValidation;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("This field is required")
                .MaximumLength(50).WithMessage("Maximum length is 50");
        }
    }
}