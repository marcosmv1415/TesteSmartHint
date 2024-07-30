using TesteSmartHint.Database;
using TesteSmartHint.Models;
using TesteSmartHint.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System;
using X.PagedList;

namespace TesteSmartHint.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        IConfiguration _configuration;
        TesteSmartHintContext _banco;

        public UsuarioRepository(TesteSmartHintContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }


        public void Atualizar(Usuario usuario)
        {
            _banco.Entry(usuario).Property(a => a.DataCadastro).IsModified = false;
            _banco.Update(usuario);
            _banco.SaveChanges();
        }

        public void Cadastrar(Usuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _banco.Add(usuario);
            if (string.IsNullOrEmpty(usuario.Email))
            {
                usuario.Email = "SEM E-mail";
            }
            _banco.SaveChanges();
        }

        public void Excluir(Usuario usuario)
        {
            usuario.FlgAtivo = false;
            _banco.Update(usuario);
            _banco.SaveChanges();
        }

        public void Ativar(Usuario usuario)
        {
            usuario.FlgAtivo = true;
            _banco.Update(usuario);
            _banco.SaveChanges();
        }

        public Usuario Login(string Email, string Senha)
        {

            Usuario usuario = _banco.Usuarios.Where(m => m.Email == Email && m.Senha == Senha).FirstOrDefault();
            return usuario;
        }
        public Usuario LoginEv(string Nome, string Senha)
        {

            Usuario usuario = _banco.Usuarios.Where(m => m.Nome == Nome && m.Senha == Senha).FirstOrDefault();
            if (usuario != null)
            {
              
                
                _banco.Entry(usuario).Property(a => a.Nome).IsModified = false;
                _banco.Entry(usuario).Property(a => a.Email).IsModified = false;
                _banco.Entry(usuario).Property(a => a.Senha).IsModified = false;
                _banco.Entry(usuario).Property(a => a.FlgAtivo).IsModified = false;
                _banco.Update(usuario);
                _banco.SaveChanges();
            }

            return usuario;
        }

        public Usuario ObterUsuario(int Id)
        {

            return _banco.Usuarios.Where(p => p.Id == Id).FirstOrDefault();
        }
        public string ObterSenhasUsuario(int id)
        {
           var senhas = _banco.Usuarios
         .Where(u => u.Id == id)
         .Select(u => new { Senha = u.Senha })
         .FirstOrDefault();
         return senhas.ToJson();    
        }
       
       
    }
}
