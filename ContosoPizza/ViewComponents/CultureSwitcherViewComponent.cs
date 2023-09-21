using ContosoPizza.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace ContosoPizza.ViewComponents
{
    public class CultureSwitcherViewComponent : ViewComponent
    {
        private readonly IOptions<RequestLocalizationOptions> localizationOptions;

        public CultureSwitcherViewComponent(IOptions<RequestLocalizationOptions> localizationOptions)
        {
            this.localizationOptions = localizationOptions;
        }

        public IViewComponentResult Invoke()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var model = new CultureSwitcherModel
            {
                SupportedCultures = localizationOptions.Value.SupportedUICultures != null ? localizationOptions.Value.SupportedUICultures.ToList() : new List<System.Globalization.CultureInfo>(),
                CurrentUICulture = cultureFeature != null ? cultureFeature.RequestCulture.UICulture : new CultureInfo("en")
            };
            return View(model);
        }
    }
}
