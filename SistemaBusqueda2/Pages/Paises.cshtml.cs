using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaBusqueda2.Modelos;
using SistemaBusqueda2.Repositorios;

namespace SistemaBusqueda2.Pages
{
    public class PaisesModel : PageModel
    {
        public List<PaisListaModelo> Paises { get; set; }
        public ActionResult OnGet()
        {
            var idSession = HttpContext.Session.GetString("idSession");
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }

            //obtener los registros de la bd y cargarselos a la propiedad Paises

            var repo = new PaisRepositorio();

            this.Paises = repo.ObtenerRoles();

            return Page();
        }
    }
}
