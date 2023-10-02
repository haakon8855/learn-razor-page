using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class PizzasModel : PageModel
    {
        private readonly PizzaService _pizzaService;
        private readonly SauceService _sauceService;
        public IList<Pizza> PizzaList { get; set; } = default!;
        public IList<Sauce> SauceList { get; set; } = default!;
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;
        [BindProperty]
        public int NewPizzaSauceId { get; set; } = default!;
        public PizzaListSorter Sorter { get; set; } = default!;
        public PizzasModel(PizzaService pizzaService, SauceService sauceService)
        {
            _pizzaService = pizzaService;
            _sauceService = sauceService;
        }

        public void Initialize(int sortBy = 0)
        {
            PizzaList = _pizzaService.GetPizzas();
            SauceList = _sauceService.GetSauces().OrderBy(s => s.Name).ToList();

            Sorter = new PizzaListSorter((PizzaListSorter.SortCriteria)sortBy);
            PizzaList = Sorter.Sort(PizzaList);
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

            NewPizza.Sauce = _sauceService.GetSauce(NewPizzaSauceId);

            _pizzaService.AddPizza(NewPizza);

            PizzaList = _pizzaService.GetPizzas();
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _pizzaService.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
