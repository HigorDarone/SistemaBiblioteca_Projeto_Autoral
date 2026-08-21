using SistemaBiblioteca_Projeto_Autoral.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SistemaBiblioteca_Projeto_Autoral.Models
{
    public class Carrinho
    {
        public int Id { get; private set; }
        public Usuario UsuarioLogado { get; private set; }
        private AppDbContext context;


        // construtor que recebe o usuário logado e o contexto do banco de dados, adiciona o carrinho no banco de dados e salva as alterações
        public Carrinho(Usuario usuarioLogado, AppDbContext context)
        {
            this.UsuarioLogado = usuarioLogado;
            this.context = context;

            context.Carrinhos.Add(this);
            context.SaveChanges();
        }

        private Carrinho() { }


        //recebe o item do carrinho e verifica se já existe um item com o mesmo id,
        //se sim, apenas aumenta a quantidade, caso contrário adiciona o item na lista
        public void AdicionarItemCarrinho(ItemCarrinho item)
        {

            if(context.ItemCarrinhos.Any(itens => itens.Livro.Id == item.Livro.Id && itens.CarrinhoId == this.Id))
            {
                var itemExistente = context.ItemCarrinhos.First(i => i.Livro.Id == item.Livro.Id && i.CarrinhoId == this.Id);
                itemExistente.Quantidade += item.Quantidade;
                context.SaveChanges();
                return;
            }
             
            item.DefinirCarrinhoId(this.Id);
            context.ItemCarrinhos.Add(item);
            context.SaveChanges();

        }


        //recebe o id do item e remove a quantidade do item no carrinho,
        //se a quantidade for menor ou igual a 0, remove o item da lista
        public string RemoverQuantidadeItemCarrinho(int id)
        {

            var itemcarrinhoremover = context.ItemCarrinhos.FirstOrDefault(i => i.Livro.Id == id && i.CarrinhoId == this.Id);
            

            if(itemcarrinhoremover == null)
            {
                return "Item não encontrado no carrinho.";  
            }

            if(itemcarrinhoremover.Quantidade <= 1)
            {
                context.ItemCarrinhos.Remove(itemcarrinhoremover);
                context.SaveChanges();
                return "Item removido com sucesso.";
            }
            
                itemcarrinhoremover.Quantidade--;
                context.SaveChanges();
                return "Quantidade removida do item.";
            
            
        }

        //recebe o id do item e remove o item do carrinho
        public string RemoverItemCarrinho(int id)
        {
            
            if(context.ItemCarrinhos.Any(i => i.Livro.Id == id && i.CarrinhoId == this.Id))
            {
                var itemCarrinho = context.ItemCarrinhos.First(i => i.Livro.Id == id && i.CarrinhoId == this.Id);
                context.ItemCarrinhos.Remove(itemCarrinho);
                context.SaveChanges();
                return "Item removido com sucesso.";
            }
            return "Item não encontrado no carrinho.";
            
        }


        //calcula o total do carrinho,
        //multiplicando o preço do livro pela quantidade de cada item e somando todos os itens
        public decimal CalcularTotal()
        { 
            return context.ItemCarrinhos
                .Where(item => item.CarrinhoId == this.Id)
                .Sum(item => item.Livro.Preco * item.Quantidade);
        }


        // finaliza o carrinho, criando uma lista de itens do pedido a partir dos itens do carrinho e salva o pedido com o preço congelado da compra,
        // para que o preço não seja alterado caso o preço do livro mude no futuro
        public Pedido FinalizarCarrinho()
        {
            var listacarrinho = context.ItemCarrinhos.Where(i => i.CarrinhoId == this.Id)
                .Include(i => i.Livro)
                .ToList();



            if (listacarrinho.Count() == 0)
            {
                throw new InvalidOperationException("O carrinho está vazio. Não é possível finalizar o pedido.");
            }

            List<ItemPedido> itensPedido = new List<ItemPedido>();

            foreach (var itemCarrinho in listacarrinho)
            {
                itensPedido.Add(new ItemPedido(itemCarrinho.Livro, itemCarrinho.Quantidade, itemCarrinho.Livro.Preco));
            }

            Pedido pedidofinalizado = new Pedido(UsuarioLogado, itensPedido);

            context.ItemCarrinhos.RemoveRange(listacarrinho);
            context.Pedidos.Add(pedidofinalizado);
            context.SaveChanges();

            return pedidofinalizado;
        }
        public ItemCarrinho BuscarPorId(int id)
        {
            return context.ItemCarrinhos.
                FirstOrDefault(i => i.Livro.Id == id && i.CarrinhoId == this.Id);
        }

        public List<ItemCarrinho> ListarItensCarrinho()
        {

            return context.ItemCarrinhos.Where(i => i.CarrinhoId == this.Id)
            .Include(i => i.Livro)
            .ToList();
           
        }

       
    }
}
