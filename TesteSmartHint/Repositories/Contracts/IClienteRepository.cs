using TesteSmartHint.Models;
using X.PagedList;

namespace TesteSmartHint.Repositories.Contracts
{
    public interface IClienteRepository
    {
        Cliente Login(string Email, string Senha);
        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int Id);
        void Bloquear(int Id);
        void Ativar(Cliente cliente);
        Cliente ObterCliente(int Id);
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa);
        bool VerificaExistenciacpf(string Cpf_Cnpj, int? Id);
        bool VerificaExistenciaEmail(string email, int? id);
        bool VerificaExistenciaInscricaoEstadual(string inscricaoEstadual, int? id);

    }
}
