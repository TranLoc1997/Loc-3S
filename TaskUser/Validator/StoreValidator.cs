using FluentValidation;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Store;

namespace TaskUser.Validator
{
    public class StoreValidator:AbstractValidator<StoreViewModels>
    {
        public  StoreValidator(IStoreService storeService,IUserService userService,SharedViewLocalizer<StoreResource> localizer)
        {
            RuleFor(x => x.Email).Must((reg, c) => !storeService.IsExistedEmailStore(reg.Id, reg.Email))
                .WithMessage(localizer.GetLocalizedString("msg_EmailAlreadyExists"));
//            RuleFor(x => x.Email).Must((reg, c) => !storeService.IsExistedEmailUser(reg.Id, reg.Email))
//                .WithMessage(localizer.GetLocalizedString("msg_EmailAlreadyExists"));
            RuleFor(x => x.StoreName).Length(1, 100).WithMessage(localizer.GetLocalizedString("msg_LengthFrom1To100Characters"));
            RuleFor(x => x.StoreName).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
            RuleFor(x => x.Email).Length(1, 100).WithMessage(localizer.GetLocalizedString("msg_LengthFrom1To100Characters"));
            RuleFor(x => x.Email).EmailAddress().NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizer.GetLocalizedString("msg_EmailIsNotAvalidEmailAddress"));
            RuleFor(x => x.Phone).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));  
        }
    }
}