using TesteSmartHint.Libraries.Filtro;
using TesteSmartHint.Libraries.Login;
using TesteSmartHint.Repositories;
using TesteSmartHint.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using X.PagedList;
using TesteSmartHint.Models;
using TesteSmartHint.Models.ViewModels;
using TesteSmartHint.Libraries.Lang;
namespace TesteSmartHint.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        IClienteRepository _clienteRepository;
        private LoginCliente _loginCliente;

        public HomeController(IClienteRepository clienteRepository, LoginCliente loginCliente)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = loginCliente;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Models.Cliente cliente)
        {

            Models.Cliente ClienteDB = new Models.Cliente();

            cliente.Senha = _loginCliente.GerarHashMd5(cliente.Senha);

            ClienteDB = _clienteRepository.Login(cliente.Email, cliente.Senha);
            
            if (ClienteDB != null)
            {
                
                if (ClienteDB.FlgAtivo == true)
                {
                    ViewData["MSG_E"] = Mensagem.MSG_E011;
                    return View();
                }
                _loginCliente.Login(ClienteDB);
                _clienteRepository.Login(cliente.Email, cliente.Senha);
                return RedirectToAction(nameof(PainelCliente));
            }
            else
            {
                ViewData["MSG_E"] = Mensagem.MSG_E010;
                return View();
            }
        }

        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginCliente.Logout();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult PainelCliente()
        {
            return View();
        }

    }
}
