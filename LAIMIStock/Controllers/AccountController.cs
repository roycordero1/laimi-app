using System;
using System.Linq;
using System.Web.Mvc;
using LAIMIStock.Models;
using System.Text;
using System.Security.Cryptography;

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
                var user = db.Usuarios.Where(x => x.nombre == userModel.nombre).FirstOrDefault();
                string salt = user.salt;
                string passSh;
                if (salt != null)
                {
                    passSh = sha256encrypt(userModel.password, salt, userModel.nombre)[0];
                }

                else
                {
                    passSh = sha256encrypt(userModel.password, "", userModel.nombre)[0];
                }

                var userDetails = db.Usuarios.Where(x => x.nombre == userModel.nombre && x.password == passSh).FirstOrDefault();


                if (userDetails == null)
                {
                    userModel.UpdatePasswordErrorMessage = "La contraseña no corresponde al usuario";
                    return View("ChangePassword", userModel);
                }
                else
                {
                    string[] pass = sha256encrypt(userModel.newPassword, "", userModel.nombre);
                    userDetails.password = pass[0];
                    userDetails.salt = pass[1];
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
        private static string[] sha256encrypt(string password, string pSalt, string userName)
        {
            string salt = "";
            if (pSalt == "")
            {
                salt = CreateSalt(userName);
            }
            else
            {
                salt = pSalt;
            }
            string saltAndPwd = String.Concat(password, salt);
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
            string hashedPwd = String.Concat(byteArrayToString(hashedDataBytes), salt);
            string[] pass = { hashedPwd, salt};
            return pass;
        }
        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        private static string CreateSalt(string UserName)
        {
            string username = UserName;
            byte[] userBytes; string salt;
            userBytes = ASCIIEncoding.ASCII.GetBytes(username);
            long XORED = 0x00; foreach (int x in userBytes)
                XORED = XORED ^ x;
            Random rand = new Random(Convert.ToInt32(XORED));
            salt = rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            return salt;
        }
    }
}