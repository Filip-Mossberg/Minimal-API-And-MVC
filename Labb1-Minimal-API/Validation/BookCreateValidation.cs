using FluentValidation;
using Labb1_Minimal_API.Models.DTOs;

namespace Labb1_Minimal_API.Validation
{
    public class BookCreateValidation : AbstractValidator<BookCreateDTO>
    {
        public BookCreateValidation()
        {
            RuleFor(model => model.Title).NotEmpty().MaximumLength(40);
            RuleFor(model => model.Author).NotEmpty().MaximumLength(40);
            RuleFor(model => model.Genre).NotEmpty().MaximumLength(40);
            RuleFor(model => model.IsAvalible).NotNull();
            RuleFor(model => model.Description).NotEmpty().MaximumLength(200);
        }
    }
}
