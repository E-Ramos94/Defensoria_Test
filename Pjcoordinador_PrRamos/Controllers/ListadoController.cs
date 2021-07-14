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
            List<ListadoViewModel> lst;
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
            return View(lst);
        }

        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(NuevoViewModel model)
        {
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
                        oTabla.idEmpleadoSolicita = model.idEmpleadoSolicita;
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

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(NuevoViewModel model)
        {
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
                        oRequi.idEmpleadoSolicita = model.idEmpleadoSolicita;
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
    }
}