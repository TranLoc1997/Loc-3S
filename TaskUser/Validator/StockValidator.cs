using FluentValidation;
using Microsoft.Extensions.Localization;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.ViewsModels.Stock;

namespace TaskUser.Validator
{
   
        public class StockValidator:AbstractValidator<StockViewModels>
        {
       
            public  StockValidator(IStockService stockService, SharedViewLocalizer<StockResource> localizer)
            {
                RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage(localizer.GetLocalizedString("msg_NumberMustBeGreaterThanOrEqualToOne"));
                RuleFor(x => x.Quantity).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
                RuleFor(x => x.ProductId).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
                RuleFor(x => x.StoreId).NotNull().WithMessage(localizer.GetLocalizedString("msg_NotEmpty"));
            }
        }
 
}