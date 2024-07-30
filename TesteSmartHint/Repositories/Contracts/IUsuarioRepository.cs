using TesteSmartHint.Models;
using X.PagedList;

namespace TesteSmartHint.Repositories.Contracts
{
    public interface IUsuarioRepository
    {
        Usuario Login(string Email, string Senha);
        Usuario LoginEv(string Nome, string Senha);
        void Cadastrar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Excluir(Usuario usuario);
        void Ativar(Usuario usuario);
        string ObterSenhasUsuario(int id);
        Usuario ObterUsuario(int Id);
    }
}
