using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class SodasModel : PageModel
    {
        private readonly SodaService _service;
        public IList<Soda> SodaList { get; set; } = default!;
        [BindProperty]
        public Soda NewSoda { get; set; } = default!;
        public SodaListSorter Sorter { get; set; } = default!;

        public SodasModel(SodaService service)
        {
            _service = service;
        }

        public void Initialize(int sortBy = 0)
        {
            SodaList = _service.GetSodas();
            Sorter = new SodaListSorter((SodaListSorter.SortCriteria)sortBy);
            SodaList = Sorter.Sort(SodaList);
        }

        public void OnGet(int sortBy)
        {
            Initialize(sortBy);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                SodaList = _service.GetSodas();
                Sorter = new SodaListSorter(0);
                SodaList = Sorter.Sort(SodaList);

                return Page();
            }

            _service.AddSoda(NewSoda);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _service.DeleteSoda(id);
            return RedirectToAction("Get");
        }
    }
}
