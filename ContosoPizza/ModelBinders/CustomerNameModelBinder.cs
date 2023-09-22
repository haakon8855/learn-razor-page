using ContosoPizza.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContosoPizza.ModelBinders
{
    public class CustomerNameModelBinder : IModelBinder
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
                var customerName = new CustomerName(value);
                bindingContext.Result = ModelBindingResult.Success(customerName);
            }
            catch (ArgumentException e)
            {
                bindingContext.ModelState.AddModelError(modelName, e.Message);
            }

            return Task.CompletedTask;
        }
    }
}
