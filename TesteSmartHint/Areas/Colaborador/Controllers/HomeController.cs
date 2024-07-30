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
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace TesteSmartHint.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {

        IUsuarioRepository _usuarioRepository;
        IClienteRepository _clienteRepository;
        private LoginUsuario _loginUsuario;
        
        public HomeController(IUsuarioRepository usuarioRepository, LoginUsuario loginUsuario, IClienteRepository clienteRepository)
        {
            _usuarioRepository = usuarioRepository;
            _loginUsuario = loginUsuario;
            _clienteRepository = clienteRepository;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Usuario usuario)
        {
            Usuario ColaboradorDB = new Usuario();

            usuario.Senha = _loginUsuario.GerarHashMd5(usuario.Senha);
            
            ColaboradorDB = _usuarioRepository.Login(usuario.Email, usuario.Senha);                   

            if (ColaboradorDB != null)
            {
               _loginUsuario.Login(ColaboradorDB);
                return RedirectToAction(nameof(Painel));
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
            _loginUsuario.Logout();
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroCliente([FromForm] TesteSmartHint.Models.Cliente cliente)
        {
           
            cliente.Cpf_Cnpj = RemoverCaracteresEspeciais(cliente.Cpf_Cnpj);
            cliente.InscricaoEstadual = RemoverCaracteresEspeciais(cliente.InscricaoEstadual);
            cliente.Senha = GerarHashMd5(cliente.Senha);
            if (_clienteRepository.VerificaExistenciacpf(cliente.Cpf_Cnpj, null))
            {
                ModelState.AddModelError("Cpf_Cnpj", Mensagem.MSG_ECPF_CNPJ);
            }
            if (!String.IsNullOrEmpty(cliente.InscricaoEstadual))
            {
                if (_clienteRepository.VerificaExistenciaInscricaoEstadual(cliente.InscricaoEstadual, null))
                {
                    ModelState.AddModelError("InscricaoEstadual", Mensagem.MSG_EINSC);
                }
            }
            if (_clienteRepository.VerificaExistenciaEmail(cliente.Email, null))
            {
                ModelState.AddModelError("Email", Mensagem.MSG__EMAIL);
            }

            if (ModelState.IsValid)
            {
                _clienteRepository.Cadastrar(cliente);
                TempData["MSG_S"] = Mensagem.MSG_S006;
                return RedirectToAction(nameof(Painel));
            }

            return View(cliente);
        }
        [HttpGet]
        public IActionResult AtualizarCliente(int id)
        {
            var cliente = _clienteRepository.ObterCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Cpf_Cnpj = AplicarMascaraCpfCnpj(cliente.Cpf_Cnpj);
            cliente.InscricaoEstadual = AplicarMascaraInscricaoEstadual(cliente.InscricaoEstadual);
            cliente.Senha = "";
            return View(cliente);
        }
        [HttpPost]
        public IActionResult AtualizarCliente(int id, [FromForm] TesteSmartHint.Models.Cliente cliente)
        {
            cliente.Cpf_Cnpj = RemoverCaracteresEspeciais(cliente.Cpf_Cnpj);
            cliente.InscricaoEstadual = RemoverCaracteresEspeciais(cliente.InscricaoEstadual);

            var clienteExistente = _clienteRepository.ObterCliente(id);
            if (clienteExistente == null)
            {
                ModelState.AddModelError(string.Empty, Mensagem.MSG_E008);
                return View(cliente);
            }
            if (_clienteRepository.VerificaExistenciacpf(cliente.Cpf_Cnpj, id))
            {
                ModelState.AddModelError("Cpf_Cnpj", Mensagem.MSG_ECPF_CNPJ);
            }
            if (!string.IsNullOrEmpty(cliente.InscricaoEstadual))
            {
                if (_clienteRepository.VerificaExistenciaInscricaoEstadual(cliente.InscricaoEstadual, id))
                {
                    ModelState.AddModelError("InscricaoEstadual", Mensagem.MSG_EINSC);
                }
            }
            if (_clienteRepository.VerificaExistenciaEmail(cliente.Email, id))
            {
                ModelState.AddModelError("Email", Mensagem.MSG__EMAIL);
            }
            if (string.IsNullOrEmpty(cliente.Senha))
            {
                ModelState.Remove("Senha");
            }
                if (ModelState.IsValid)
            {

                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Email = cliente.Email;
                clienteExistente.Telefone = cliente.Telefone;
                clienteExistente.TipoPessoa = cliente.TipoPessoa;
                clienteExistente.Cpf_Cnpj = cliente.Cpf_Cnpj;
                clienteExistente.InscricaoEstadual = cliente.InscricaoEstadual;
                clienteExistente.FlgInscricaoEstadualPF = cliente.FlgInscricaoEstadualPF;
                clienteExistente.FlgAtivo = cliente.FlgAtivo;
                clienteExistente.Genero = cliente.Genero;
                clienteExistente.DataNascimento = cliente.DataNascimento;

                if (!string.IsNullOrEmpty(cliente.Senha))
                {
                    clienteExistente.Senha = GerarHashMd5(cliente.Senha);
                }

                _clienteRepository.Atualizar(clienteExistente);
                TempData["MSG_S"] = Mensagem.MSG_S007;
                return RedirectToAction(nameof(Painel));
            }

            return View(cliente);
        }
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _clienteRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Painel));
        }       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AplicarAlteracoesBloqueio(int[] ids, bool[] isBlocked)
        {
            if (ids.Length != isBlocked.Length)
            {
                return BadRequest("Dados inconsistentes.");
            }

            for (int i = 0; i < ids.Length; i++)
            {
                var cliente = _clienteRepository.ObterCliente(ids[i]);
                if (cliente != null)
                {
                    cliente.FlgAtivo = isBlocked[i]; 
                    _clienteRepository.Atualizar(cliente);
                }
            }
            TempData["MSG_E"] = Mensagem.MSG_E013;
            return RedirectToAction(nameof(Painel));
        }


        [HttpGet]
        public IActionResult Painel(int? pagina, string pesquisa)
        {    
           
           var clientes = _clienteRepository.ObterTodosClientes(pagina, pesquisa);
           

            var viewModel = new IndexViewModel
            {
                lista = clientes
            };

            return View(viewModel);
        }
        private string RemoverCaracteresEspeciais(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return new string(value.Where(char.IsDigit).ToArray());
        }

        private string AplicarMascaraCpfCnpj(string cpfCnpj)
        {
            if (string.IsNullOrEmpty(cpfCnpj)) return cpfCnpj;

            cpfCnpj = cpfCnpj.Trim();

            if (cpfCnpj.Length == 11) // CPF
            {
                return Convert.ToUInt64(cpfCnpj).ToString(@"000\.000\.000\-00");
            }
            else if (cpfCnpj.Length == 14) // CNPJ
            {
                return Convert.ToUInt64(cpfCnpj).ToString(@"00\.000\.000\/0000\-00");
            }

            return cpfCnpj;
        }

        private string AplicarMascaraInscricaoEstadual(string inscricaoEstadual)
        {
            if (string.IsNullOrEmpty(inscricaoEstadual)) return inscricaoEstadual;

            inscricaoEstadual = inscricaoEstadual.Trim();
            return Convert.ToUInt64(inscricaoEstadual).ToString(@"000\.000\.000\-000");
        }
        public string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }

}