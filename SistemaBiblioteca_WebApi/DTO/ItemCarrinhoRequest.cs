using SistemaBiblioteca_Projeto_Autoral.Models;

namespace SistemaBiblioteca_WebApi.DTO
{
    public class ItemCarrinhoRequest
    {
        public int LivroId { get; set; }

        public int Quantidade { get; set; }
    }
}
