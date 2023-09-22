using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class PizzaInfoModel : PageModel
    {
        private readonly PizzaService _pizzaService;
        private readonly ToppingService _toppingService;
        [BindProperty]
        public int Id { get; set; } = default!;
        public Pizza? Pizza { get; set; } = default!;
        public IEnumerable<Topping> ToppingList { get; set; } = default!;
        [BindProperty]
        public int ToppingToAddId { get; set; } = default!;
        public decimal TotalCalories { get; set; } = default!;
        public ToppingListSorter Sorter { get; set; } = default!;

        public PizzaInfoModel(PizzaService pizzaService, ToppingService toppingService)
        {
            _pizzaService = pizzaService;
            _toppingService = toppingService;
        }

        public void OnGet(int id, int sortBy)
        {
            Initialize(id, sortBy);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Initialize(Id);
                return Page();
            }

            var toppingToAdd = _toppingService.GetTopping(ToppingToAddId);
            Pizza = _pizzaService.GetPizza(Id);
            if (toppingToAdd == null || Pizza == null)
            {
                return Page();
            }

            _pizzaService.AddTopping(Id, toppingToAdd);
            _toppingService.AddPizza(toppingToAdd.Id, Pizza);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int toppingId)
        {
            if (toppingId < 0)
            {
                return RedirectToAction("Get");
            }

            var toppingToRemove = _toppingService.GetTopping(toppingId);
            Pizza = _pizzaService.GetPizza(Id);
            if (toppingToRemove == null || Pizza == null)
            {
                return Page();
            }

            _pizzaService.DeleteTopping(Id, toppingToRemove);
            _toppingService.DeletePizza(toppingToRemove.Id, Pizza);

            return RedirectToAction("Get");
        }
        public void Initialize(int id, int sortBy = 0)
        {
            Id = id;
            Pizza = _pizzaService.GetPizza(Id);
            ToppingList = _toppingService.GetToppings();

            Sorter = new ToppingListSorter((ToppingListSorter.SortCriteria)sortBy);

            if (Pizza != null && Pizza.Toppings != null)
            {
                // Remove toppings already on the pizza
                ToppingList = ToppingList.Where(t => !Pizza.Toppings.Contains(t)).OrderBy(t => t.Name);
                // Calculate total calories
                if (Pizza.Toppings.Count > 0)
                {
                    Sorter = new ToppingListSorter((ToppingListSorter.SortCriteria)sortBy);
                    Pizza.Toppings = Sorter.Sort(Pizza.Toppings.ToList());

                    // Have to use a foreach loop because LINQ doesn't support null conditional operators
                    TotalCalories = 0;
                    foreach (var topping in Pizza.Toppings)
                    {
                        if (topping != null && topping.Calories != null)
                        {
                            TotalCalories += (decimal)topping.Calories;
                        }
                    }
                }
            }
        }
    }
}
