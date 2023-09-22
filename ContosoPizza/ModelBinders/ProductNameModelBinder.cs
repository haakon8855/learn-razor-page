using ContosoPizza.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContosoPizza.ModelBinders
{
    public class ProductNameModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {
                var productName = new ProductName(value);
                bindingContext.Result = ModelBindingResult.Success(productName);
            }
            catch (ArgumentException e)
            {
                bindingContext.ModelState.AddModelError(modelName, e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
