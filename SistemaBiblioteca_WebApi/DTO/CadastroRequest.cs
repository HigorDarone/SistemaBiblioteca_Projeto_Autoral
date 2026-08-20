using SistemaBiblioteca_Projeto_Autoral.Utils;

namespace SistemaBiblioteca_WebApi.DTO
{
    public class CadastroRequest
    {
       
        public string Nome { get; set; }
        
        public string Documento { get; set; }

        public string Email { get; set; }
       
        public string Senha { get; set; }
    }
}
