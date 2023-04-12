using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result = BL.Cine.GetAll();

            if(result.Correct)
            {
                cine.Cines = result.Objects;
                return View(cine);
            }
            else
            {
                return View(cine);
            }
        }


        [HttpGet]
        public ActionResult From(int? idCine)
        {
            ML.Result resultZona = BL.Zona.GetAll();
            
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();

            if(resultZona.Correct)
            {
                cine.Zona.Zonas = resultZona.Objects;
            }

            if(idCine == null)
            {
                return View(cine);
            }
            else
            {
                ML.Result result = BL.Cine.GetById(idCine.Value);
                if(result.Correct)
                {
                    cine = (ML.Cine)result.Object;
                    cine.Zona.Zonas = resultZona.Objects;
                    return View(cine);
                }
                else
                {
                    ViewBag.Message = "no se pudoo consultar la infromacion";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult From(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            if(cine.IdCine == 0)
            {
                result = BL.Cine.Add(cine);
                if(result.Correct)
                {
                    ViewBag.Message = "Se Inserto el resgistro";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un problema al insertar el resgistro";
                }
                return View("Modal");
            }
            else
            {
                result = BL.Cine.Update(cine);

                if(result.Correct)
                {
                    ViewBag.Message = "Se actualizo el registro";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un probelam al actualizar la informacion";
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(int idCine)
        {
            ML.Result result = BL.Cine.Delete(idCine);
            //return View("GetAll",result);
            if(result.Correct)
            {
                ViewBag.Message = "Se elimino el resgistro";
            }
            else
            {
                ViewBag.Message = "Ocurrio un problema al eliminar el registro";
            }
            return View("Modal");
        }
    }
}
