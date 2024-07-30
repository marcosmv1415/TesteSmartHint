using TesteSmartHint.Database;
using TesteSmartHint.Models;
using TesteSmartHint.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System;
using X.PagedList;
using System.Drawing;


namespace TesteSmartHint.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        IConfiguration _configuration;
        TesteSmartHintContext _banco;

        public ClienteRepository(TesteSmartHintContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }


        public void Atualizar(Cliente cliente)
        {
            _banco.Entry(cliente).Property(a => a.DataCadastro).IsModified = false;
            _banco.Update(cliente);
            _banco.SaveChanges();
        }

        public void Cadastrar(Cliente cliente)
        {
            cliente.DataCadastro = DateTime.Now;
            _banco.Add(cliente);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Cliente cliente = ObterCliente(Id);
            _banco.Remove(cliente);
            _banco.SaveChanges();
        }
        public void Bloquear(int Id)
        {
            Cliente cliente = ObterCliente(Id);
            cliente.FlgAtivo = true;
            _banco.Update(cliente);
            _banco.SaveChanges();
        }

        public void Ativar(Cliente cliente)
        {
            cliente.FlgAtivo = true;
            _banco.Update(cliente);
            _banco.SaveChanges();
        }

        public Cliente Login(string Email, string Senha)
        {

            Cliente cliente = _banco.Clientes.Where(m => m.Email == Email && m.Senha == Senha).FirstOrDefault();
            return cliente;
        }
        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _configuration.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;
            var bancoCliente = _banco.Clientes.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoCliente = bancoCliente.Where(a => a.Nome.Contains(pesquisa.Trim()));

            }
            return bancoCliente.ToPagedList<Cliente>(NumeroPagina, RegistroPorPagina);
        }
        public bool VerificaExistenciacpf(string Cpf_Cnpj, int? Id)
        {
            if (Id == null)
            {
                return _banco.Clientes.Any(p => p.Cpf_Cnpj == Cpf_Cnpj);
            }
            else
            {
                return _banco.Clientes.Any(p => p.Cpf_Cnpj == Cpf_Cnpj && p.Id != Id);
            }
            
        }
        public bool VerificaExistenciaEmail(string email, int? id)
        {
            if (id == null)
            {                
                return _banco.Clientes.Any(p => p.Email == email);
            }
            else
            {
                return _banco.Clientes.Any(p => p.Email == email && p.Id != id);
            }
        }
        public bool VerificaExistenciaInscricaoEstadual(string inscricaoEstadual, int? id)
        {
            if (id == null)
            {
                return _banco.Clientes.Any(p => p.InscricaoEstadual == inscricaoEstadual);
            }
            else
            {
                return _banco.Clientes.Any(p => p.InscricaoEstadual == inscricaoEstadual && p.Id != id);
            }
        }

        public Cliente ObterCliente(int Id)
        {

            return _banco.Clientes.Where(p => p.Id == Id).FirstOrDefault();
        }
    }
}