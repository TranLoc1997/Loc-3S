using FluentValidation;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Category;

namespace TaskUser.Validator
{
    public class CategoryValidator:AbstractValidator<CategoryViewsModels>
    {
        
        public CategoryValidator(SharedViewLocalizer<CategoryResource> localizer ,ICategoryService  categoryService)
        {
            RuleFor(x => x.CategoryName).Must((reg, c) => !categoryService.IsExistedName(reg.Id, reg.CategoryName))
                .WithMessage(localizer.GetLocalizedString("msg_NameCategoryAlreadyExists"));
            RuleFor(x => x.CategoryName).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));

        }
        
    }
}