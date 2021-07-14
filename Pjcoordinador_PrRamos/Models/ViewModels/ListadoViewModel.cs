using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pjcoordinador_PrRamos.Models.ViewModels
{
    public class ListadoViewModel
    {
        public int idRequisicion { get; set; }
        public string detalleRequisicion { get; set; }
        public decimal cantidad { get; set; }
        public decimal precioUnitaro { get; set; }
        public decimal total { get; set; }
        public string idEmpleadoSolicita { get; set; }
        public DateTime fechaSolicita { get; set; }
        public string Estado { get; set; }
        public DateTime fechaGrabada { get; set; }
        public int idEmpleado { get; set; }
        public int idEstado { get; set; }
    }
}