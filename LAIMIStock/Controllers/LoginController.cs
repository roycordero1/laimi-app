using System;
using System.Linq;
using System.Web.Mvc;
using LAIMIStock.Models;
using System.Text;
using System.Security.Cryptography;

namespace LAIMIStock.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Usuarios userModel)
        {
            using (laimistockappEntities db = new laimistockappEntities())
            {
                var user = db.Usuarios.Where(x => x.nombre == userModel.nombre).FirstOrDefault();
                string salt = user.salt;
                string passSh;
                if (salt != null)
                {
                    passSh = sha256encrypt(userModel.password, salt, userModel.nombre);
                }

                else
                {
                    passSh = sha256encrypt(userModel.password, "", userModel.nombre);
                }
                
                var userDetails = db.Usuarios.Where(x => x.nombre == userModel.nombre && x.password == passSh).FirstOrDefault();

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Usuario o contraseña incorrectos";
                    return View("Index", userModel);
                }
                else
                {
                    
                    Session["usuarioID"] = userDetails.idUsuario;
                    Session["nombre"] = userDetails.nombre;
                    if (Convert.ToInt32(Session["usuarioID"]) == 2)
                    {
                        return RedirectToAction("OperadorView", "Operator");
                    }
                    else
                    {
                        var alerts = db.Suministros.Where(x => x.cantidad <= x.limiteSuministro).ToList();
                        Session["Alerts"] = alerts.Count;
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
            }

        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        private static string sha256encrypt(string password, string pSalt, string userName)
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
            return hashedPwd;
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