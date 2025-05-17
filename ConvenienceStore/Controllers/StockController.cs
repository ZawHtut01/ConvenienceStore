using ConvenienceStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConvenienceStore.Controllers
{
    public class StockController : Controller
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        // GET: Stock/StockIn
        public async Task<IActionResult> StockIn()
        {
            var items = await _stockService.GetAllItemsAsync();
            return View(items);
        }

        // POST: Stock/StockIn
        [HttpPost]
        public async Task<IActionResult> StockIn(int itemId, int quantity)
        {
            if (quantity <= 0)
            {
                TempData["ErrorMessage"] = "Quantity must be greater than zero.";
                return RedirectToAction(nameof(StockIn));
            }

            var result = await _stockService.StockInAsync(itemId, quantity);

            if (result.Success)
                TempData["SuccessMessage"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(StockIn));
        }

        // GET: Stock/StockOut
        public async Task<IActionResult> StockOut()
        {
            var items = await _stockService.GetAllItemsAsync();
            return View(items);
        }

        // POST: Stock/StockOut
        [HttpPost]
        public async Task<IActionResult> StockOut(int itemId, int quantity)
        {
            if (quantity <= 0)
            {
                TempData["ErrorMessage"] = "Quantity must be greater than zero.";
                return RedirectToAction(nameof(StockOut));
            }

            var result = await _stockService.StockOutAsync(itemId, quantity);

            if (result.Success)
                TempData["SuccessMessage"] = result.Message;
            else
                TempData["ErrorMessage"] = result.Message;

            return RedirectToAction(nameof(StockOut));
        }
    }
}