using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class ToppingsModel : PageModel
    {
        private readonly ToppingService _service;
        public IList<Topping> ToppingList { get; set; } = default!;
        [BindProperty]
        public Topping NewTopping { get; set; } = default!;
        public ToppingListSorter Sorter { get; set; } = default!;

        public ToppingsModel(ToppingService service)
        {
            _service = service;
        }

        public void OnGet(int sortBy)
        {
            Initialize(sortBy);
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                Initialize();
                return Page();
            }

            _service.AddTopping(NewTopping);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteTopping(id);
            return RedirectToAction("Get");
        }

        public void Initialize(int sortBy = 0)
        {
            ToppingList = _service.GetToppings();
            Sorter = new ToppingListSorter((ToppingListSorter.SortCriteria)sortBy);
            ToppingList = Sorter.Sort(ToppingList);
        }
    }
}
