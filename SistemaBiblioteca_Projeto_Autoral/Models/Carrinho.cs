using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Carrinho
    {

        public Usuario UsuarioLogado { get; private set; }


        //lista privada para guardar informações do carrinho, não pode ser alterada externamente    
        private List<ItemCarrinho> itenscarrinho = new List<ItemCarrinho>();


        //lista pública para acessar os itens do carrinho, mas não permite alteração direta
        public List<ItemCarrinho> ItensCarrinho
        {
            get { return new List<ItemCarrinho>(itenscarrinho); }
        }


        //recebe o item do carrinho e verifica se já existe um item com o mesmo id,
        //se sim, apenas aumenta a quantidade, caso contrário adiciona o item na lista
        public void AdicionarItemCarrinho(ItemCarrinho item)
        {
            foreach (var itemCarrinho in itenscarrinho)
            {
                if (itemCarrinho.Livro.Id == item.Livro.Id)
                {
                    itemCarrinho.Quantidade += item.Quantidade;
                    return;
                }
            }
            itenscarrinho.Add(item);
        }


        //recebe o id do item e remove a quantidade do item no carrinho,
        //se a quantidade for menor ou igual a 0, remove o item da lista
        public string RemoverQuantidadeItemCarrinho(int id)
        {
            foreach (var itemCarrinho in itenscarrinho)
            {

                itemCarrinho.Quantidade--;
                if (itemCarrinho.Livro.Id == id)
                {
                    if (itemCarrinho.Quantidade <= 0)
                    {
                        itenscarrinho.Remove(itemCarrinho);
                    }
                    
                    return "Item removido com sucesso.";
                }
            }
            return "Item não encontrado no carrinho.";
        }

        //recebe o id do item e remove o item do carrinho
        public string RemoverItemCarrinho(int id)
        {
            foreach (var itemCarrinho in itenscarrinho)
            {
                if (itemCarrinho.Livro.Id == id)
                {
                    itenscarrinho.Remove(itemCarrinho);
                    return "Item removido com sucesso.";
                }
            }
            return "Item não encontrado no carrinho.";
        }


        //calcula o total do carrinho,
        //multiplicando o preço do livro pela quantidade de cada item e somando todos os itens
        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var itemCarrinho in itenscarrinho)
            {
                total += itemCarrinho.Livro.Preco * itemCarrinho.Quantidade;
            }
            return total;
        }


        // finaliza o carrinho, criando uma lista de itens do pedido a partir dos itens do carrinho e salva o pedido com o preço congelado da compra,
        // para que o preço não seja alterado caso o preço do livro mude no futuro
        public Pedido FinalizarCarrinho()
        {
            List<ItemPedido> itensPedido = new List<ItemPedido>();

            if(itenscarrinho.Count == 0)
            {
                throw new InvalidOperationException("O carrinho está vazio. Não é possível finalizar o pedido.");
            }

            foreach (var itemCarrinho in itenscarrinho)
            {
                itensPedido.Add(new ItemPedido(itemCarrinho.Livro, itemCarrinho.Quantidade, itemCarrinho.Livro.Preco));
            }

            itenscarrinho.Clear();

            return new Pedido(UsuarioLogado, itensPedido);

        }

        //contrutor da classe Carrinho, recebe o usuário logado e inicializa a lista de itens do carrinho
        public Carrinho(Usuario usuario)
        {
            UsuarioLogado = usuario;

           
        }
    }
}
