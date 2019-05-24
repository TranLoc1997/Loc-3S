using FluentValidation;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Brand;

namespace TaskUser.Validator
{
    public class BrandValidator:AbstractValidator<BrandViewsModels>
    {
        public BrandValidator(SharedViewLocalizer<BrandResource> localizer ,IBrandService  brandService)
        {
            RuleFor(x => x.BrandName).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));  
            RuleFor(x => x.BrandName).Must((reg, c) => !brandService.IsExistedName(reg.Id, reg.BrandName))
                .WithMessage((reg, c) => string.Format(localizer.GetLocalizedString("msg_NameBrandAlreadyExists"),c));
        }
    }
}