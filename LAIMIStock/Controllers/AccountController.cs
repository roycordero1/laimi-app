using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAIMIStock.Models;

namespace LAIMIStock.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult ChangePassword()
        {
                return View();
        }

        public ActionResult UpdatePassword(Usuarios userModel)
        {
            using (laimistockappEntities db = new laimistockappEntities())
            {
                var userDetails = db.Usuarios.Where(x => x.nombre == userModel.nombre && x.password == userModel.password).FirstOrDefault();
                
                
                if (userDetails == null)
                {
                    userModel.UpdatePasswordErrorMessage = "La contraseña no corresponde al usuario";
                    return View("ChangePassword", userModel);
                }
                else
                {
                    userDetails.password = userModel.newPassword;
                    userDetails.nombre = userModel.nombre;
                    try
                    {
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting
                                // the current instance as InnerException
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                    TempData["msg"] = "<script>alert('Contraseña cambiada con éxito');</script>";
                    return View("ChangePassword");
                }
                
            }
            
        }
    }
}