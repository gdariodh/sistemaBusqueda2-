using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaBusqueda2.Repositorios;

namespace SistemaBusqueda2.Pages
{
    public class NuevoUsuarioModel : PageModel
    {
        [BindProperty]
        [Display(Name ="Nombre de usuario")]
        [Required(ErrorMessage ="El campo Nombre de usuario es requerido")]
        public string NombreUsuario { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo Nombres es requerido")]
        public string Nombres { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "El campo Apellidos es requerido")]
        public string Apellidos { get; set; }
        [BindProperty]
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo Rol es requerido")]
        public int? RolId { get; set; }
        [BindProperty]
        [Display(Name = "Contraseņa")]
        [Required(ErrorMessage = "El campo Contraseņa es requerido")]
        [MinLength(8,ErrorMessage ="La contraseņa debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$",ErrorMessage ="La contraseņa debe tener al menos una Mayuscula, minusculas y digitos")]
        public string Password { get; set; }
        [BindProperty]
        [Display(Name = "Repetir contraseņa")]
        [Required(ErrorMessage = "El campo Repetir contraseņa es requerido")]
        [MinLength(8, ErrorMessage = "La contraseņa debe tener al menos 8 caracteres")]
        [RegularExpression("^(?=\\w*\\d)(?=\\w*[A-Z])(?=\\w*[a-z])\\S{8,16}$", ErrorMessage = "La contraseņa debe tener al menos una Mayuscula, minusculas y digitos")]
        public string RePassword { get; set; }
        public ActionResult OnGet()
        {
            var idSession = HttpContext.Session.GetString("idSession");
            if (string.IsNullOrEmpty(idSession))
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var password = this.Password;
                var repassword = this.RePassword;
                //Valido si las contraseņas son iguales
                if (password != repassword)
                {
                    ModelState.AddModelError(string.Empty, "Las contraseņas no coinciden");
                    return Page();
                }
                var repo = new UsuarioRepositorio();
                //Validar que no exista el nombre de usuario
                if (repo.ExisteNombreUsuario(this.NombreUsuario))
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario ya se encuentra registrado en la base de datos");
                    return Page();
                }

                //Guardar el usuario en la BD

                repo.InsertarUsuario(this.Nombres, this.Apellidos, (int)this.RolId, this.NombreUsuario, this.Password);
                return RedirectToPage("./Usuarios");
            }

            return Page();
        }

    }
}
