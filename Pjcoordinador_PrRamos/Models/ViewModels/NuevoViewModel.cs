using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pjcoordinador_PrRamos.Models.ViewModels
{
    public class NuevoViewModel
    {
        public int idRequisicion { get; set; }
        [Required]
        [Display(Name ="Detalle")]
        [StringLength(200)]
        public string detalleRequisicion { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        [Range(0, int.MaxValue, ErrorMessage = "Ingresar solo numeros enteros")]
        public decimal cantidad { get; set; }
        [Required]
        [Display(Name = "Precio Unitario")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public decimal precioUnitaro { get; set; }
        [Required]
        [Display(Name = "Total")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        public decimal total { get; set; }
        [Required]
        [Display(Name = "Empleado solicitante")]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un numero de Id porfavor")]
        public int idEmpleadoSolicita { get; set; }
        public int idEstado { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de requisición")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime fechaSolicita { get; set; }
        public System.DateTime fechaGrabada { get; set; }
    }
}