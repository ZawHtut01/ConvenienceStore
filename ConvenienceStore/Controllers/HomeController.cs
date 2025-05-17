using ConvenienceStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConvenienceStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly StockService _stockService;

        public HomeController(StockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _stockService.GetAllItemsAsync();
            return View(items);
        }
    }
}