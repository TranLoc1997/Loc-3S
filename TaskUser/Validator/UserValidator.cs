using FluentValidation;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.User;

namespace TaskUser.Validator
{
    public class UserValidator:AbstractValidator<UserViewsModels>
    {
        public  UserValidator(IUserService userService, SharedViewLocalizer<UserResource> localizer){
            
            RuleFor(x => x.Email).Must((reg, c) => !userService.IsExistedEmailUser(reg.Id, reg.Email))
                .WithMessage(localizer.GetLocalizedString("msg_EmailAlreadyExists"));
            
            RuleFor(x => x.Name).Length(1, 100).WithMessage(localizer.GetLocalizedString("msg_LengthFrom1To100Characters"));
            RuleFor(x => x.Name).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
            RuleFor(x => x.PassWord).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
            RuleFor(x => x.PassWord).MinimumLength(6).WithMessage(localizer.GetLocalizedString("msg_MinimumLengthOf6Characters"));
            RuleFor(x => x.PassWord).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage(localizer.GetLocalizedString("msg_PasswordMustContainBothLetterSandNumbers."));
            RuleFor(x => x.Email).Length(1, 100).WithMessage(localizer.GetLocalizedString("msg_LengthFrom1To100Characters"));
            RuleFor(x => x.Email).EmailAddress().NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty") );
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizer.GetLocalizedString("msg_EmailIsNotAvalidEmailAddress"));
            RuleFor(x => x.Phone).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));            
            RuleFor(x => x.IsActiver).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
        } 
    }
}