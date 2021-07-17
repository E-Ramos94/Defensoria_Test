using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pjcoordinador_PrRamos.Models;
using Pjcoordinador_PrRamos.Models.ViewModels;

namespace Pjcoordinador_PrRamos.Models
{
    public class ListadoController : Controller
    {
        // GET: Listado
        public ActionResult Index()
        {
            List<ListadoViewModel> lst = null;
            int userid = (int)Session["IdUser"];
            int puesto = (int)Session["IdPuesto"];

            if (puesto == 1) {
            using (DBcoordinador_RamosEntities4 db = new DBcoordinador_RamosEntities4())
            {
                //lst = (from d in db.View_requisiciones
                //       select new ListadoViewModel
                lst = (from d in db.View_requisiciones
                       where d.idEmpleadoSolicita == userid
                       select new ListadoViewModel
                       {
                           idRequisicion = d.idRequisicion,
                           detalleRequisicion = d.detalleRequisicion,
                           cantidad = d.cantidad,
                           precioUnitaro = d.precioUnitario,
                           total = d.total,
                           idEmpleadoSolicita = d.usuario,
                           fechaSolicita = d.fechaSolicita,
                           Estado = d.estado,
                           fechaGrabada = d.fechaGraba
                       }).ToList();
            }
            }
            else if (puesto == 2)
            {
                using (DBcoordinador_RamosEntities4 db = new DBcoordinador_RamosEntities4())
                {
                    lst = (from d in db.View_requisiciones
                           select new ListadoViewModel
                           {
                               idRequisicion = d.idRequisicion,
                               detalleRequisicion = d.detalleRequisicion,
                               cantidad = d.cantidad,
                               precioUnitaro = d.precioUnitario,
                               total = d.total,
                               idEmpleadoSolicita = d.usuario,
                               fechaSolicita = d.fechaSolicita,
                               Estado = d.estado,
                               fechaGrabada = d.fechaGraba
                           }).ToList();
                }
            }
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(NuevoViewModel model)
        {
            int userid = (int)Session["IdUser"];
            try
            {
                if (ModelState.IsValid)
                {
                    using (DBcoordinador_RamosEntities2 db = new DBcoordinador_RamosEntities2())
                    {
                        var oTabla = new t_Requisiciones();
                        oTabla.detalleRequisicion = model.detalleRequisicion;
                        oTabla.cantidad = model.cantidad;
                        oTabla.precioUnitario = model.precioUnitaro;
                        oTabla.total = model.total;
                        oTabla.idEmpleadoSolicita = userid;
                        oTabla.fechaSolicita = model.fechaSolicita;
                        oTabla.idEstado = 1;
                        oTabla.fechaGraba = DateTime.Now;

                        db.t_Requisiciones.Add(oTabla);
                        db.SaveChanges();
                    }

                    return Redirect("~/Listado/");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int Id)
        {
            NuevoViewModel model = new NuevoViewModel();
            List<ListadoViewModel> lst = null;
            using (DBcoordinador_RamosEntities2 db = new DBcoordinador_RamosEntities2())
            {
                var oRequi = db.t_Requisiciones.Find(Id);
                model.idRequisicion = oRequi.idRequisicion;
                model.detalleRequisicion = oRequi.detalleRequisicion;
                model.cantidad = oRequi.cantidad;
                model.precioUnitaro = oRequi.precioUnitario;
                model.total = oRequi.total;
                model.fechaSolicita = oRequi.fechaSolicita;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(NuevoViewModel model)
        {
            int userid = (int)Session["IdUser"];
            try
            {
                if (ModelState.IsValid)
                {
                    using (DBcoordinador_RamosEntities2 db = new DBcoordinador_RamosEntities2())
                    {
                        var oRequi = db.t_Requisiciones.Find(model.idRequisicion);
                        oRequi.detalleRequisicion = model.detalleRequisicion;
                        oRequi.cantidad = model.cantidad;
                        oRequi.precioUnitario = model.precioUnitaro;
                        oRequi.total = model.total;
                        oRequi.idEmpleadoSolicita = userid;
                        oRequi.fechaSolicita = model.fechaSolicita;
                        oRequi.idEstado = 1;
                        oRequi.fechaGraba = DateTime.Now;

                        db.Entry(oRequi).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return Redirect("~/Listado/");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            using (DBcoordinador_RamosEntities2 db = new DBcoordinador_RamosEntities2())
            {
                var oRequi = db.t_Requisiciones.Find(Id);
                db.t_Requisiciones.Remove(oRequi);
                db.SaveChanges();
            }
            return Redirect("~/Listado/");
        }

        public ActionResult Revision(int Id)
        {
            List<ListadoViewModel> lst = null;
            NuevoViewModel model = new NuevoViewModel();
            using (DBcoordinador_RamosEntities2 db = new DBcoordinador_RamosEntities2())
            {
                var oRequi = db.t_Requisiciones.Find(Id);
                model.idRequisicion = oRequi.idRequisicion;
                model.detalleRequisicion = oRequi.detalleRequisicion;
                model.cantidad = oRequi.cantidad;
                model.precioUnitaro = oRequi.precioUnitario;
                model.total = oRequi.total;
                model.fechaSolicita = oRequi.fechaSolicita;
                model.idEmpleadoSolicita = oRequi.idEmpleadoSolicita;
                model.idEstado = oRequi.idEstado;

                lst =
                    (from d in db.c_Estado
                     select new ListadoViewModel
                     {
                         idEstado = d.idEstado,
                         estado = d.estado
                     }).ToList();

            }

            List<SelectListItem> item = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.estado.ToString(),
                    Value = d.idEstado.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = item;

            return View(model);
        }

        [HttpPost]
        public ActionResult Revision(NuevoViewModel model, FormCollection form)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    using (DBcoordinador_RamosEntities2 db = new DBcoordinador_RamosEntities2())
                    {
                        string State = form["Estados"];
                        var oRequi = db.t_Requisiciones.Find(model.idRequisicion);
                        oRequi.idEstado = int.Parse(State);
                        oRequi.fechaGraba = DateTime.Now;

                        db.Entry(oRequi).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    int userid = (int)Session["IdUser"];
                    using (DBcoordinador_RamosEntities5 db = new DBcoordinador_RamosEntities5())
                    {
                        string State = form["Estados"];

                        var oTabla = new t_RequisicionesBitacora();
                        oTabla.idRequisicion = model.idRequisicion;
                        oTabla.idEstadoAnterior = model.idEstado;
                        oTabla.idEstadoNuevo = int.Parse(State);
                        oTabla.fechaRegistro = DateTime.Now;
                        oTabla.observacion = "SE HICIERON CAMBIOS";
                        oTabla.idEmpleadoRegistra = userid;

                        db.t_RequisicionesBitacora.Add(oTabla);
                        db.SaveChanges();
                    }

                    return Redirect("~/Listado/");
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