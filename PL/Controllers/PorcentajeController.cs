using BL;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PorcentajeController : Controller
    {
        public IActionResult Grafica()
        {
            
            ML.Result result = BL.Cine.Calculo();
            ML.Result resultCine = BL.Cine.GetAll();

            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();

            if (result.Correct && resultCine.Correct)
            {
                cine.Zona = (ML.Zona)result.Object;
                cine.Cines= resultCine.Objects;
            }
            return View(cine);
        }
    }
}
