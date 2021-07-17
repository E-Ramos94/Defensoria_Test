using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pjcoordinador_PrRamos.Models;
using Pjcoordinador_PrRamos.Models.ViewModels;

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

        public ActionResult Registros()
        {
            List<RegistroViewModel> lst = null;
            RegistroViewModel model = new RegistroViewModel();

            using (DBcoordinador_RamosEntities3 db = new DBcoordinador_RamosEntities3())
            {
                lst =
                    (from d in db.c_Puesto
                     select new RegistroViewModel
                     {
                         idpuesto = d.idPuesto,
                         puesto = d.puesto
                     }).ToList();
            }

            List<SelectListItem> item = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.puesto.ToString(),
                    Value = d.idpuesto.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = item;

            return View(model);
        }
        [HttpPost]
        public ActionResult Registros(RegistroViewModel model, FormCollection form)
        {
            //int userid = (int)Session["IdUser"];
            try
            {
                if (ModelState.IsValid)
                {
                    using (DBcoordinador_RamosEntities3 db = new DBcoordinador_RamosEntities3())
                    {
                        string Puesto = form["Puestos"];
                        var oTabla = new c_Empleado();
                        oTabla.nombres = model.nombre;
                        oTabla.apellidos = model.apellido;
                        oTabla.usuario = model.usuario;
                        oTabla.clave = model.clave;
                        oTabla.idPuesto = int.Parse(Puesto);
                        oTabla.estaActivo = true;

                        db.c_Empleado.Add(oTabla);
                        db.SaveChanges();
                    }

                    return Redirect("~/Access/");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}