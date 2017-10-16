using System.Linq;
using System.Web.Mvc;
using LAIMIStock.Models;


namespace LAIMIStock.Controllers
{
    public class OperatorController : Controller
    {
        // GET: Operator
        public ActionResult OperadorView(Suministros supply)
        {
            laimistockappEntities db = new laimistockappEntities();
            supply.supplies = db.Suministros.Select(x => new SelectListItem
            {
                Value = x.codigo,
                Text = x.nombre
            });

            int SelectedValue = supply.selectedSupply;

            if (SelectedValue != 0)
            {
                var valoresSuministro = db.Suministros.Where(x => x.idSuministro == SelectedValue).FirstOrDefault();
                if (valoresSuministro != null)
                {
                    supply.cantidad = valoresSuministro.cantidad;
                }
                else
                {
                    //Response.Write("<script>alert(" + SelectedValue + ")</script>");
                }
                
            }

            return View(supply);
        }

        [HttpPost]
        public ActionResult UseSupply(Suministros supply)
        {
            laimistockappEntities db = new laimistockappEntities();

            int SelectedValueId = supply.selectedSupply;
            

            if (SelectedValueId != 0)
            {
                var valoresSuministro = db.Suministros.Where(x => x.idSuministro == SelectedValueId).FirstOrDefault();
                if (valoresSuministro != null)
                {
                    valoresSuministro.cantidad = valoresSuministro.cantidad - 1;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('Consumo exitoso');</script>";
                }
                else
                {
                    
                    Response.Write("<script>alert(No hay suficiente suministros para consumir)</script>");
                }

                return RedirectToAction("OperadorView", "Operator");

            }
            else
            {
                return RedirectToAction("OperadorView", "Operator");
            }

            
        }
    }

}