using TesteSmartHint.Models;
using TesteSmartHint.Libraries.Sessao;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace TesteSmartHint.Libraries.Login
{
    public class LoginUsuario
    {
        private string Key = "Login.Usuario";
        private Sessao.Sessao _sessao;

        public LoginUsuario(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Usuario usuario)
        {

            string usuarioJSONString = JsonConvert.SerializeObject((usuario), Formatting.Indented,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    }
);



            _sessao.Cadastrar(Key, usuarioJSONString);
        }
        
        public Usuario GetUsuario()
        {
            if (_sessao.Existe(Key))
            {
                string usuarioJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Usuario>(usuarioJSONString);
            }
            else
            {
                return null;
            }
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
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
       
    }
}
