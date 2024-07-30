using TesteSmartHint.Libraries.Login;
using TesteSmartHint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteSmartHint.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private bool _tipoColaboradorAutorizado;
        public ColaboradorAutorizacaoAttribute(bool TipoColaboradorAutorizado = true)
        {
            _tipoColaboradorAutorizado = TipoColaboradorAutorizado;
        }


        LoginUsuario _loginUsuario;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginUsuario = (LoginUsuario)context.HttpContext.RequestServices.GetService(typeof(LoginUsuario));
            Usuario usuario = _loginUsuario.GetUsuario();
            if (usuario == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if (_tipoColaboradorAutorizado == usuario.FlgAtivo)
                {
                    context.Result = new ForbidResult();
                }
            }
        }

    }
}

