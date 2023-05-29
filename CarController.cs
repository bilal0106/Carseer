using Carseer.Mangers;
using Microsoft.AspNetCore.Mvc;

namespace Carseer.Controllers
{
    public class CarController : Controller
    {
        CarManager carManager;

        public CarController()
        {
            carManager = new CarManager();
        }
        public IActionResult Index()
        {
            var makesList = carManager.GetAllMakes();
            return View(makesList);
        }

        public IActionResult LoadData(string makeId, string year)
        {
            try
            {

               if (!string.IsNullOrEmpty(makeId) && !string.IsNullOrEmpty(year)) 
                {
                    var makesList = carManager.GetModelsForMakeIdYear(makeId, year);
                    return View(makesList);
                }
                return null;
            }
            catch (Exception)
            {
                ViewBag["Ërror"] = "Ensure about your entered year";
                return null;
            }

        }

    }
}
