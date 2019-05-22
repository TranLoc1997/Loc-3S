using FluentValidation;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels;
using TaskUser.ViewsModels.User;

namespace TaskUser.Validator
{
    public class PasswordValidator : AbstractValidator<EditViewPassword>
    {
        public  PasswordValidator(IUserService userService,SharedViewLocalizer<PasswordResource> localizer)
        {
            RuleFor(x => x.NewPassword).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
            RuleFor(x => x.NewPassword).MinimumLength(6).WithMessage(localizer.GetLocalizedString("msg_MinimumLengthOf6Characters"));
            RuleFor(x => x.NewPassword).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage(localizer.GetLocalizedString("msg_PasswordMustContainBothLetterSandNumbers."));
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
            RuleFor(x => x.ConfirmPassword).MinimumLength(6).WithMessage(localizer.GetLocalizedString("msg_MinimumLengthOf6Characters"));
            RuleFor(x => x.ConfirmPassword).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage(localizer.GetLocalizedString("msg_PasswordMustContainBothLetterSandNumbers."));
            RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword)
                .WithMessage(localizer.GetLocalizedString("msg_ThePasswordDoesNotMatch"));
            

        }
        
    }
    
}

