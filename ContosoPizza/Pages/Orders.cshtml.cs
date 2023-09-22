using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoPizza.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly OrderService _orderService;
        private readonly PizzaService _pizzaService;
        private readonly SodaService _sodaService;
        public IList<Order> OrderList { get; set; } = default!;
        public IEnumerable<Pizza> PizzaList { get; set; } = default!;
        public IEnumerable<Soda> SodaList { get; set; } = default!;
        [BindProperty]
        public Order NewOrder { get; set; } = default!;
        [BindProperty]
        public int NewOrderPizzaId { get; set; } = default!;
        [BindProperty]
        public int NewOrderSodaId { get; set; } = default!;
        public OrderListSorter Sorter { get; set; } = default!;

        public OrdersModel(OrderService orderService, PizzaService pizzaService, SodaService sodaService)
        {
            _orderService = orderService;
            _pizzaService = pizzaService;
            _sodaService = sodaService;
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

            NewOrder.Pizza = _pizzaService.GetPizza(NewOrderPizzaId);
            NewOrder.Soda = _sodaService.GetSoda(NewOrderSodaId);

            _orderService.AddOrder(NewOrder);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostAdvance(int id)
        {
            _orderService.AdvanceOrderStatus(id);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostRevert(int id)
        {
            _orderService.RevertOrderStatus(id);
            return RedirectToAction("Get");
        }

        public void Initialize(int sortBy = 0)
        {
            OrderList = _orderService.GetOrders();
            PizzaList = _pizzaService.GetPizzas().OrderBy(p => p.Name);
            SodaList = _sodaService.GetSodas().OrderBy(s => s.Name);

            Sorter = new OrderListSorter((OrderListSorter.SortCriteria)sortBy);
            OrderList = Sorter.Sort(OrderList);
        }
    }
}
