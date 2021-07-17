using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pjcoordinador_PrRamos.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required]
        [Display(Name = "Ingrese sus nombres")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Ingrese sus apellidos")]
        public string apellido { get; set; }
        [Required]
        [Display(Name = "Ingrese su nombre de usuario")]
        public string usuario { get; set; }
        [Required]
        [Display(Name = "Ingrese su contraseña")]
        [DataType(DataType.Password)]
        public string clave { get; set; }
        [Required]
        [Display(Name = "Confirme su contraseña")]
        [DataType(DataType.Password)]
        [Compare("clave")]
        public string clave2 { get; set; }
        [Required]
        [Display(Name = "Seleccionar tipo de puesto")]
        public int idpuesto { get; set; }
        public string puesto { get; set; }
    }
}