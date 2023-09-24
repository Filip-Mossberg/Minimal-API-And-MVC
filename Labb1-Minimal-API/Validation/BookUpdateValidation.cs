using FluentValidation;
using Labb1_Minimal_API.Data;
using Labb1_Minimal_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Labb1_Minimal_API.Validation
{
    public class BookUpdateValidation : AbstractValidator<BookUpdateDTO>
    {
        private AppDbContext _appDbContext;
        public BookUpdateValidation(AppDbContext context)
        {
            this._appDbContext = context;

            RuleFor(models => models.Id).Must(IsUniqeId).WithMessage("Person with given id already exists!");
            RuleFor(model => model.Title).NotEmpty().MaximumLength(40);
            RuleFor(model => model.Author).NotEmpty().MaximumLength(40);
            RuleFor(model => model.Genre).NotEmpty().MaximumLength(40);
            RuleFor(model => model.IsAvalible).NotNull();
            RuleFor(model => model.Description).NotEmpty().MaximumLength(200);
        }

        private bool IsUniqeId(int id)
        {
            return _appDbContext.book.Any(b => b.Id == id);
        }
    }
}
