using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pjcoordinador_PrRamos.Models;
namespace Pjcoordinador_PrRamos.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                using (DBcoordinador_RamosEntities3 db = new DBcoordinador_RamosEntities3())
                {
                    var lst = from d in db.c_Empleado
                              where d.usuario == user && d.clave == password && d.estaActivo == true
                              select d;

                    foreach (var category in lst)
                    {
                        Session["IdUser"] = category.idEmpleado;
                        Session["IdPuesto"] = category.idPuesto;
                    }

                    if  (lst.Count()>0)
                    {
                        c_Empleado oUser = lst.First();
                        Session["User"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario invalido");
                    }
                }
                
            }
            catch (Exception ex)
            {
                return Content("Ocurrio un error" + ex.Message);
            }
        }
    }
}