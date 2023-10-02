using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class SaucesModel : PageModel
    {
        private readonly SauceService _service;
        public IList<Sauce> SauceList { get; set; } = default!;
        [BindProperty]
        public Sauce NewSauce { get; set; } = default!;
        public SauceListSorter Sorter { get; set; } = default!;

        public SaucesModel(SauceService service)
        {
            _service = service;
        }

        public void Initialize(int sortBy = 0)
        {

            SauceList = _service.GetSauces();
            Sorter = new SauceListSorter((SauceListSorter.SortCriteria)sortBy);
            SauceList = Sorter.Sort(SauceList);
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

            _service.AddSauce(NewSauce);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteSauce(id);
            return RedirectToAction("Get");
        }
    }
}
