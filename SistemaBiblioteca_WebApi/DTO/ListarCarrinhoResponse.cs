using SistemaBiblioteca_Projeto_Autoral.Models;

namespace SistemaBiblioteca_WebApi.DTO
{
    public class ListarCarrinhoResponse
    {
        public List<ItemCarrinho> ItensCarrinho { get; set; }
        public decimal ValorTotal { get; set;}

        public ListarCarrinhoResponse(List<ItemCarrinho> itensCarrinho, decimal valorTotal)
        {
            ItensCarrinho = itensCarrinho;
            ValorTotal = valorTotal;
        }
    }
}
