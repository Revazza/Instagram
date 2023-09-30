using FluentValidation;
using Instagram.Application.Common.Enums;

namespace Instagram.Application.Commands.Stories.AddStory;

public class AddStoryCommandValidator : AbstractValidator<AddStoryCommand>
{
    private const int MAX_FILE_SIZE = (int)FileSize.TenMegabytes;
    public AddStoryCommandValidator()
    {
        RuleFor(x => x.File)
            .NotNull();
        RuleFor(x => x.File.Length)
            .NotEqual(0);
        RuleFor(x => x.File.Length)
            .LessThan(MAX_FILE_SIZE)
            .WithMessage("File size must be less than 10mb");

    }
}